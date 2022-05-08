const statusesRepository = require('../repositories/statuses.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const statuses = await statusesRepository.getAllStatuses();

    return {
      status: Status.OK,
      content: statuses,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingStatus = await statusesRepository.getStatusesById(id);

  if (existingStatus === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve status with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingStatus,
  };
};

const create = async (status) => {
  status.statusEntries = [];

  const savedStatus = await statusesRepository.createStatuses(status);

  if (Object.keys(savedStatus) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create status'
    );
  }

  return {
    status: Status.CREATED,
    content: savedStatus,
  };
};

const update = async (id, status) => {
  const existingStatus = await statusesRepository.getStatusesById(id);

  if (existingStatus === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve status with specified identifier'
    );
  }

  const updatedStatus = await statusesRepository.updateStatuses(id, status);

  if (Object.keys(updatedStatus) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update status'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingStatus = await statusesRepository.getStatusesById(id);

  if (existingStatus === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve status with specified identifier'
    );
  }

  await statusesRepository.deleteStatuses(id);

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
