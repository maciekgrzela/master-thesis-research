const coursesRepository = require('../repositories/courses.repository');
const courseCategoriesRepository = require('../repositories/courseCategories.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const courses = await coursesRepository.getAllCourses();

    return {
      status: Status.OK,
      content: courses,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getIngredientsForCourseId = async (id) => {
  const existingCourse = await coursesRepository.getCourseById(id);

  if (existingCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve course with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingCourse.ingredients,
  };
};

const getById = async (id) => {
  const existingCourse = await coursesRepository.getCourseById(id);

  if (existingCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve course with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingCourse,
  };
};

const create = async (course) => {
  const existingCourseCategory =
    await courseCategoriesRepository.getCourseCategoryById(
      course.courseCategoryId
    );

  if (existingCourseCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  let ingredientsList = [];
  course.orderedCourses = [];

  for (const ingredient in course.ingredients) {
    const existingProduct = await productsRepository.getProductById(
      ingredient.productId
    );

    if (existingProduct === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to retrieve courseCategory with specified identifier'
      );
    }

    ingredientsList.push({
      amount: ingredient.amount,
      product: existingProduct,
    });
  }

  course.ingredients = ingredientsList;

  const savedCourse = await coursesRepository.createCourse(course);

  if (Object.keys(savedCourse) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create course'
    );
  }

  return {
    status: Status.CREATED,
    content: savedCourse,
  };
};

const update = async (id, course) => {
  const existingCourse = await coursesRepository.getCourseById(id);

  if (existingCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve course with specified identifier'
    );
  }

  const existingCourseCategory =
    await courseCategoriesRepository.getCourseCategoryById(
      course.courseCategoryId
    );

  if (existingCourseCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  let ingredientsList = [];

  for (const ingredient in course.ingredients) {
    const existingProduct = await productsRepository.getProductById(
      ingredient.productId
    );

    if (existingProduct === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to retrieve courseCategory with specified identifier'
      );
    }

    ingredientsList.push({
      amount: ingredient.amount,
      product: existingProduct,
    });
  }

  course.ingredients = ingredientsList;

  const updatedCourse = await coursesRepository.updateCourse(id, course);

  if (Object.keys(updatedCourse) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update course'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingCourse = await coursesRepository.getCourseById(id);

  if (existingCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve course with specified identifier'
    );
  }

  await coursesRepository.deleteCourse(id);

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

module.exports = {
  getAll,
  getIngredientsForCourseId,
  getById,
  create,
  deleteById,
  update,
};
