const billsRepository = require('../repositories/bills.repository');
const orderedCoursesRepository = require('../repositories/orderedCourses.repository');
const ordersRepository = require('../repositories/orders.repository');
const customersRepository = require('../repositories/customers.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async (pageNumber, pageSize) => {
  try {
    const bills = await billsRepository.getAllBills(pageNumber, pageSize);

    return {
      status: Status.OK,
      content: bills,
    };
  } catch (e) {
    console.log(e);
    throw new BusinessLogicError(Status.INTERNAL_ERROR, e.content);
  }
};

const getById = async (id) => {
  const existingBill = await billsRepository.getBillById(id);

  if (existingBill === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve bill with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingBill,
  };
};

const getOrderedCoursesForBillId = async (id) => {
  const existingBill = await billsRepository.getBillById(id);

  if (existingBill === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve bill with specified identifier'
    );
  }

  const courses = await orderedCoursesRepository.getOrderedCoursesForBillId(id);

  return {
    status: Status.OK,
    content: courses,
  };
};

const create = async (bill) => {
  try {
    const existingOrder = await ordersRepository.getOrdersById(bill.orderId);

    if (existingOrder === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to create bill. Order with specified identifier does not exist'
      );
    }

    if (bill.customerId !== null) {
      const existingCustomer = await customersRepository.getCustomerById(
        bill.customerId
      );

      if (existingCustomer === null) {
        throw new BusinessLogicError(
          Status.NOT_FOUND,
          'Unable to create bill. Customer with specified identifier does not exist'
        );
      }
    }

    bill.grossPrice = 0;
    bill.netPrice = 0;
    bill.orderedCourses = undefined;

    const savedBill = await billsRepository.createBill(bill);

    if (Object.keys(savedBill) === 0) {
      throw new BusinessLogicError(
        Status.INTERNAL_ERROR,
        'Unable to create bill'
      );
    }

    return {
      status: Status.CREATED,
      content: savedBill,
    };
  } catch (e) {
    console.log(e);
    return {
      status: Status.INTERNAL_ERROR,
      content: e.message,
    };
  }
};

const update = async (id, bill) => {
  const existingBill = await billsRepository.getBillById(id);

  if (existingBill === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve bill with specified identifier'
    );
  }

  if (bill.orderId !== null) {
    const existingOrder = await ordersRepository.getOrdersById(bill.orderId);

    if (existingOrder === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to create bill. Order with specified identifier does not exist'
      );
    }

    bill.order = existingOrder;
  }

  if (bill.customerId !== null) {
    const existingCustomer = await customersRepository.getCustomerById(
      bill.customerId
    );

    if (existingCustomer === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to create bill. Customer with specified identifier does not exist'
      );
    }

    bill.customer = existingCustomer;
  }

  const updatedBill = await billsRepository.updateBill(id, bill);

  if (Object.keys(updatedBill) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update bill'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingBill = await billsRepository.getBillById(id);

  if (existingBill === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve bill with specified identifier'
    );
  }

  await billsRepository.deleteBill(id);

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
  getOrderedCoursesForBillId,
};
