const productCategoriesRepository = require('../repositories/productCategories.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const productCategories =
      await productCategoriesRepository.getAllProductCategories();

    return {
      status: Status.OK,
      content: productCategories,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingProductCategory =
    await productCategoriesRepository.getProductCategoriesById(id);

  if (existingProductCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve productCategory with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingProductCategory,
  };
};

const create = async (productCategory) => {
  productCategory.products = [];

  const savedProductCategory =
    await productCategoriesRepository.createProductCategories(productCategory);

  if (Object.keys(savedProductCategory) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create productCategory'
    );
  }

  return {
    status: Status.CREATED,
    content: savedProductCategory,
  };
};

const update = async (id, productCategory) => {
  const existingProductCategory =
    await productCategoriesRepository.getProductCategoriesById(id);

  if (existingProductCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve productCategory with specified identifier'
    );
  }

  const updatedProductCategory =
    await productCategoriesRepository.updateProductCategories(
      id,
      productCategory
    );

  if (Object.keys(updatedProductCategory) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update productCategory'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingProductCategory =
    await productCategoriesRepository.getProductCategoriesById(id);

  if (existingProductCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve productCategory with specified identifier'
    );
  }

  await productCategoriesRepository.deleteProductCategories(id);

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
