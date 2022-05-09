const ingredientsRepository = require('../repositories/ingredients.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const ingredients = await ingredientsRepository.getAllIngredients();

    return {
      status: Status.OK,
      content: ingredients,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingIngredient = await ingredientsRepository.getIngredientsById(id);

  if (existingIngredient === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve ingredient with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingIngredient,
  };
};

const create = async (ingredient) => {
  const existingProduct = await productsRepository.getProductById(
    ingredient.productId
  );

  if (existingProduct === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  ingredient.product = existingProduct;

  if (ingredient.courseId !== null) {
    const existingCourse = await coursesRepository.getCourseById(
      ingredient.courseId
    );

    if (existingCourse === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to retrieve course with specified identifier'
      );
    }

    ingredient.course = existingCourse;
  }

  const savedIngredient = await ingredientsRepository.createIngredients(
    ingredient
  );

  if (Object.keys(savedIngredient) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create ingredient'
    );
  }

  return {
    status: Status.CREATED,
    content: savedIngredient,
  };
};

const update = async (id, ingredient) => {
  const existingIngredient = await ingredientsRepository.getIngredientsById(id);

  if (existingIngredient === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve ingredient with specified identifier'
    );
  }

  const existingProduct = await productsRepository.getProductById(
    ingredient.productId
  );

  if (existingProduct === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  ingredient.product = existingProduct;

  if (ingredient.courseId !== null) {
    const existingCourse = await coursesRepository.getCourseById(
      ingredient.courseId
    );

    if (existingCourse === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to retrieve course with specified identifier'
      );
    }

    ingredient.course = existingCourse;
  }

  const updatedIngredient = await ingredientsRepository.updateIngredients(
    id,
    ingredient
  );

  if (Object.keys(updatedIngredient) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update ingredient'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingIngredient = await ingredientsRepository.getIngredientsById(id);

  if (existingIngredient === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve ingredient with specified identifier'
    );
  }

  await ingredientsRepository.deleteIngredients(id);

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
