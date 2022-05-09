const reservationsRepository = require('../repositories/reservations.repository');
const tablesRepository = require('../repositories/tables.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const reservations = await reservationsRepository.getAllReservations();

    return {
      status: Status.OK,
      content: reservations,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingReservation = await reservationsRepository.getReservationsById(
    id
  );

  if (existingReservation === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve reservation with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingReservation,
  };
};

const create = async (reservation) => {
  const existingTable = await tablesRepository.getTablesById(
    reservation.tableId
  );

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  reservation.table = existingTable;

  const savedReservation = await reservationsRepository.createReservations(
    reservation
  );

  if (Object.keys(savedReservation) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create reservation'
    );
  }

  return {
    status: Status.CREATED,
    content: savedReservation,
  };
};

const update = async (id, reservation) => {
  const existingReservation = await reservationsRepository.getReservationsById(
    id
  );

  if (existingReservation === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve reservation with specified identifier'
    );
  }

  const existingTable = await tablesRepository.getTablesById(
    reservation.tableId
  );

  if (existingTable === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve table with specified identifier'
    );
  }

  reservation.table = existingTable;

  const updatedReservation = await reservationsRepository.updateReservations(
    id,
    reservation
  );

  if (Object.keys(updatedReservation) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update reservation'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingReservation = await reservationsRepository.getReservationsById(
    id
  );

  if (existingReservation === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve reservation with specified identifier'
    );
  }

  await reservationsRepository.deleteReservations(id);

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
