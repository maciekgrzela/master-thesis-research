const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllCustomers = async () => {
  return await client.customer.findMany({
    include: { bills: true },
  });
};

const getCustomerById = async (id) => {
  return await client.customer.findUnique({
    where: { id: id },
    include: { bills: true },
  });
};

const createCustomer = async (customer) => {
  return await client.customer.create({
    data: customer,
  });
};

const updateCustomer = async (id, customer) => {
  return await client.customer.update({
    where: {
      id: Number(id),
    },
    data: customer,
  });
};

const deleteCustomer = async (id) => {
  return await client.customer.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllCustomers,
  getCustomerById,
  createCustomer,
  updateCustomer,
  deleteCustomer,
};
