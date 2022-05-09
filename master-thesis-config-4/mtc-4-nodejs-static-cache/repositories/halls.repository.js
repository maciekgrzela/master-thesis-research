const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllHalls = async () => {
  return await client.hall.findMany({
    include: { tables: true },
  });
};

const getHallById = async (id) => {
  return await client.hall.findUnique({
    where: { id: id },
    include: { tables: true },
  });
};

const createHall = async (hall) => {
  return await client.hall.create({
    data: hall,
  });
};

const updateHall = async (id, hall) => {
  return await client.hall.update({
    where: {
      id: Number(id),
    },
    data: hall,
  });
};

const deleteHall = async (id) => {
  return await client.hall.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllHalls,
  getHallById,
  createHall,
  updateHall,
  deleteHall,
};
