const productsRepository = require('../repositories/products.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const products = await productsRepository.getAllProducts();

    return {
      status: Status.OK,
      content: products,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingProduct = await productsRepository.getProductsById(id);

  if (existingProduct === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve product with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingProduct,
  };
};

const create = async (product) => {
  product.ingredients = [];

  const existingProductCategory =
    await productCategoriesRepository.getProductCategoriesById(
      product.productsCategoryId
    );

  if (existingProductCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve productCategory with specified identifier'
    );
  }

  product.productsCategory = existingProductCategory;

  const savedProduct = await productsRepository.createProducts(product);

  if (Object.keys(savedProduct) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create product'
    );
  }

  return {
    status: Status.CREATED,
    content: savedProduct,
  };
};

const update = async (id, product) => {
  const existingProduct = await productsRepository.getProductById(id);

  if (existingProduct === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve product with specified identifier'
    );
  }

  const existingProductCategory =
    await productCategoriesRepository.getProductCategoriesById(
      product.productsCategoryId
    );

  if (existingProductCategory === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve productCategory with specified identifier'
    );
  }

  product.productsCategory = existingProductCategory;

  const updatedProduct = await productsRepository.updateProducts(id, product);

  if (Object.keys(updatedProduct) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update product'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingProduct = await productsRepository.getProductById(id);

  if (existingProduct === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve product with specified identifier'
    );
  }

  await productsRepository.deleteProducts(id);

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
