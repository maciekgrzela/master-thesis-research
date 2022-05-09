const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllStatuses = async () => {
  return await client.status.findMany({
    include: { statusEntries: true },
  });
};

const getStatusesById = async (id) => {
  return await client.status.findUnique({
    where: { id: id },
    include: { statusEntries: true },
  });
};

const createStatuses = async (status) => {
  return await client.status.create({
    data: status,
  });
};

const updateStatuses = async (id, status) => {
  return await client.status.update({
    where: {
      id: Number(id),
    },
    data: status,
  });
};

const deleteStatuses = async (id) => {
  return await client.status.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllStatuses,
  getStatusesById,
  createStatuses,
  updateStatuses,
  deleteStatuses,
};
