const tablesRepository = require('../repositories/tables.repository');
const hallsRepository = require('../repositories/halls.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const tables = await tablesRepository.getAllTables();

    return {
      status: Status.OK,
      content: tables,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingTable = await tablesRepository.getTablesById(id);

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingTable,
  };
};

const create = async (table) => {
  table.orders = [];
  table.reservations = [];

  const existingHall = await hallsRepository.getHallById(table.hallId);

  if (existingHall === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve hall with specified identifier'
    );
  }

  table.hall = existingHall;

  const savedTable = await tablesRepository.createTables(table);

  if (Object.keys(savedTable) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create table'
    );
  }

  return {
    status: Status.CREATED,
    content: savedTable,
  };
};

const update = async (id, table) => {
  const existingTable = await tablesRepository.getTablesById(id);

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  const existingHall = await hallsRepository.getHallById(table.hallId);

  if (existingHall === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve hall with specified identifier'
    );
  }

  table.hall = existingHall;

  const updatedTable = await tablesRepository.updateTables(id, table);

  if (Object.keys(updatedTable) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update table'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingTable = await tablesRepository.getTablesById(id);

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  await tablesRepository.deleteTables(id);

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
