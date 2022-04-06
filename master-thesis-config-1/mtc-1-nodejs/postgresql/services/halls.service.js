const hallsRepository = require('../repositories/halls.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const halls = await hallsRepository.getAllHalls();

    return {
      status: Status.OK,
      content: halls,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingHall = await hallsRepository.getHallById(id);

  if (existingHall === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve hall with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingHall,
  };
};

const getTablesForHallId = async (id) => {
  const existingHall = await hallsRepository.getHallById(id);

  if (existingHall === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve hall with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingHall.tables,
  };
};

const create = async (hall) => {
  hall.tables = [];

  const savedHall = await hallsRepository.createHall(hall);

  if (Object.keys(savedHall) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create hall'
    );
  }

  return {
    status: Status.CREATED,
    content: savedHall,
  };
};

const update = async (id, hall) => {
  const existingHall = await hallsRepository.getHallById(id);

  if (existingHall === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve hall with specified identifier'
    );
  }

  const updatedHall = await hallsRepository.updateHall(id, hall);

  if (Object.keys(updatedHall) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update hall'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingHall = await hallsRepository.getHallById(id);

  if (existingHall === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve hall with specified identifier'
    );
  }

  await hallsRepository.deleteHall(id);

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
  getTablesForHallId,
};
