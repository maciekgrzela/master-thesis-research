const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllProductCategories = async () => {
  return await client.productsCategory.findMany({
    include: { products: true },
  });
};

const getProductCategoriesById = async (id) => {
  return await client.productsCategory.findUnique({
    where: { id: id },
    include: { products: true },
  });
};

const createProductCategories = async (productsCategory) => {
  return await client.productsCategory.create({
    data: productsCategory,
  });
};

const updateProductCategories = async (id, productsCategory) => {
  return await client.productsCategory.update({
    where: {
      id: Number(id),
    },
    data: productsCategory,
  });
};

const deleteProductCategories = async (id) => {
  return await client.productsCategory.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllProductCategories,
  getProductCategoriesById,
  createProductCategories,
  updateProductCategories,
  deleteProductCategories,
};
