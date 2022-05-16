const courseCategoriesRepository = require('../repositories/courseCategories.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const categories =
      await courseCategoriesRepository.getAllCourseCategories();

    return {
      status: Status.OK,
      content: categories,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingCourseCategory =
    await courseCategoriesRepository.getCourseCategoryById(id);

  if (existingCourseCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingCourseCategory,
  };
};

const create = async (courseCategory) => {
  courseCategory.courses = [];

  const savedCourseCategory =
    await courseCategoriesRepository.createCourseCategory(courseCategory);

  if (Object.keys(savedCourseCategory) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create courseCategory'
    );
  }

  return {
    status: Status.CREATED,
    content: savedCourseCategory,
  };
};

const update = async (id, courseCategory) => {
  const existingCourseCategory =
    await courseCategoriesRepository.getCourseCategoryById(id);

  if (existingCourseCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  const updatedCourseCategory =
    await courseCategoriesRepository.updateCourseCategory(id, courseCategory);

  if (Object.keys(updatedCourseCategory) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update courseCategory'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingCourseCategory =
    await courseCategoriesRepository.getCourseCategoryById(id);

  if (existingCourseCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  await courseCategoriesRepository.deleteCourseCategory(id);

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

module.exports = {
  getAll,
  getById,
  create,
  deleteById,
  update,
};
