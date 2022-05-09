const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllIngredients = async () => {
  return await client.ingredient.findMany({
    include: { course: true, product: true },
  });
};

const getIngredientsById = async (id) => {
  return await client.ingredient.findUnique({
    where: { id: id },
    include: { course: true, product: true },
  });
};

const createIngredients = async (ingredient) => {
  return await client.ingredient.create({
    data: ingredient,
  });
};

const updateIngredients = async (id, ingredient) => {
  return await client.ingredient.update({
    where: {
      id: Number(id),
    },
    data: ingredient,
  });
};

const deleteIngredients = async (id) => {
  return await client.ingredient.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllIngredients,
  getIngredientsById,
  createIngredients,
  updateIngredients,
  deleteIngredients,
};
