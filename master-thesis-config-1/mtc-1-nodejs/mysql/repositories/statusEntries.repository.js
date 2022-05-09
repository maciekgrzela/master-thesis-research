const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllStatusEntries = async () => {
  return await client.statusEntry.findMany({
    include: { order: true, orderedCourse: true, status: true },
  });
};

const getStatusEntriesById = async (id) => {
  return await client.statusEntry.findUnique({
    where: { id: id },
    include: { order: true, orderedCourse: true, status: true },
  });
};

const createStatusEntries = async (statusEntry) => {
  return await client.statusEntry.create({
    data: statusEntry,
  });
};

const updateStatusEntries = async (id, statusEntry) => {
  return await client.statusEntry.update({
    where: {
      id: Number(id),
    },
    data: statusEntry,
  });
};

const deleteStatusEntries = async (id) => {
  return await client.statusEntry.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllStatusEntries,
  getStatusEntriesById,
  createStatusEntries,
  updateStatusEntries,
  deleteStatusEntries,
};
