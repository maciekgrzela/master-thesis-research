const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllOrders = async () => {
  return await client.order.findMany({
    include: {
      bills: true,
      orderedCourses: true,
      statusEntries: true,
      table: true,
      user: true,
    },
  });
};

const getOrdersById = async (id) => {
  return await client.order.findUnique({
    where: { id: id },
    include: {
      bills: true,
      orderedCourses: true,
      statusEntries: true,
      table: true,
      user: true,
    },
  });
};

const getTableOrders = async (id) => {
  return await client.order.findMany({
    where: { id: id },
    include: {
      bills: true,
      orderedCourses: true,
      statusEntries: true,
      table: true,
      user: true,
    },
  });
};

const getUserOrders = async (id) => {
  return await client.order.findMany({
    where: { userId: id },
    include: {
      bills: true,
      orderedCourses: true,
      statusEntries: true,
      table: true,
      user: true,
    },
  });
};

const createOrders = async (order) => {
  return await client.order.create({
    data: order,
  });
};

const updateOrders = async (id, order) => {
  return await client.order.update({
    where: {
      id: Number(id),
    },
    data: order,
  });
};

const deleteOrders = async (id) => {
  return await client.order.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllOrders,
  getOrdersById,
  getTableOrders,
  getUserOrders,
  createOrders,
  updateOrders,
  deleteOrders,
};
