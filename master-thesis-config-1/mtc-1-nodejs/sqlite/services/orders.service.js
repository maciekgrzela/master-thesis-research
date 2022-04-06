const ordersRepository = require('../repositories/orders.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const orders = await ordersRepository.getAllOrders();

    return {
      status: Status.OK,
      content: orders,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingOrder = await ordersRepository.getOrderById(id);

  if (existingOrder === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve order with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingOrder,
  };
};

const getLastTableOrder = async (id) => {
  const existingTable = await tablesRepository.getTablesById(id);

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  const orders = await ordersRepository.getTableOrders(id);
  const lastOrder = orders.sort((a, b) => a.created - b.created)[0];

  return {
    status: Status.OK,
    content: lastOrder,
  };
};

const getTableOrders = async (id) => {
  const existingTable = await tablesRepository.getTablesById(id);

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  const orders = await ordersRepository.getTableOrders(id);

  return {
    status: Status.OK,
    content: orders,
  };
};

const getUserOrders = async (id) => {
  const existingUser = await usersRepository.getUserById(order.userId);

  if (existingUser === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve user with specified identifier'
    );
  }

  const orders = await ordersRepository.getUserOrders(id);

  return {
    status: Status.OK,
    content: orders,
  };
};

const create = async (order) => {
  let newOrder = { ...order };

  newOrder.orderedCourse = [];
  newOrder.statusEntries = [];

  const existingTable = await tablesRepository.getTablesById(order.tableId);

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  newOrder.table = existingTable;

  const existingUser = await usersRepository.getUserById(order.userId);

  if (existingUser === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve user with specified identifier'
    );
  }

  newOrder.user = existingUser;

  const savedOrder = await ordersRepository.createOrders(newOrder);

  if (Object.keys(savedOrder) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create order'
    );
  }

  const orderedStatus = await statusesRepository.getStatusByName('ordered');
  const createdStatus = await statusesRepository.getStatusByName('created');

  const statusEntryForOrder = {
    note: '',
    status: orderedStatus,
    statusId: orderedStatus.id,
    order: newOrder,
    orderId: newOrder.id,
  };

  await statusEntriesRepository.createStatusEntries(statusEntryForOrder);

  for (const orderedCourse in order.orderedCourse) {
    await orderedCoursesRepository.createOrderedCourse(orderedCourse);

    const statusEntryForOrderedCourse = {
      note: '',
      status: createdStatus,
      statusId: createdStatus.id,
      orderedCourse: orderedCourse,
      orderedCourseId: orderedCourse.id,
    };

    await statusEntriesRepository.createStatusEntries(
      statusEntryForOrderedCourse
    );
  }

  return {
    status: Status.CREATED,
    content: savedOrder,
  };
};

const update = async (id, order) => {
  const existingOrder = await ordersRepository.getOrdersById(id);

  if (existingOrder === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve order with specified identifier'
    );
  }

  const updatedOrder = await ordersRepository.updateOrders(id, order);

  if (Object.keys(updatedOrder) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update order'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

module.exports = {
  getAll,
  getById,
  getLastTableOrder,
  getTableOrders,
  getUserOrders,
  create,
  update,
};
