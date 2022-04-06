const customersRepository = require('../repositories/customers.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const customers = await customersRepository.getAllCustomers();

    return {
      status: Status.OK,
      content: customers,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingCustomer = await customersRepository.getCustomerById(id);

  if (existingCustomer === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingCustomer,
  };
};

const create = async (customer) => {
  customer.bills = [];

  const savedCustomer = await customersRepository.createCustomer(customer);

  if (Object.keys(savedCustomer) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create customer'
    );
  }

  return {
    status: Status.CREATED,
    content: savedCustomer,
  };
};

const update = async (id, customer) => {
  const existingCustomer = await customersRepository.getCustomerById(id);

  if (existingCustomer === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  const updatedCustomer = await customersRepository.updateCustomer(
    id,
    customer
  );

  if (Object.keys(updatedCustomer) === 0) {
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
  const existingCustomer = await customersRepository.getCustomerById(id);

  if (existingCustomer === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve courseCategory with specified identifier'
    );
  }

  await customersRepository.deleteCustomer(id);

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
