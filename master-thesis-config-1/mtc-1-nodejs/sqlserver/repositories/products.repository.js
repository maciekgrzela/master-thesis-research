const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllProducts = async () => {
  return await client.product.findMany({
    include: { ingredients: true, productsCategory: true },
  });
};

const getProductsById = async (id) => {
  return await client.product.findUnique({
    where: { id: id },
    include: { ingredients: true, productsCategory: true },
  });
};

const createProducts = async (product) => {
  return await client.product.create({
    data: product,
  });
};

const updateProducts = async (id, product) => {
  return await client.product.update({
    where: {
      id: Number(id),
    },
    data: product,
  });
};

const deleteProducts = async (id) => {
  return await client.product.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllProducts,
  getProductsById,
  createProducts,
  updateProducts,
  deleteProducts,
};
