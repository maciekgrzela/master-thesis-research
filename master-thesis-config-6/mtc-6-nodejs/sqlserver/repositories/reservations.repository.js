const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllReservations = async () => {
  return await client.reservation.findMany({
    include: { table: true },
  });
};

const getReservationsById = async (id) => {
  return await client.reservation.findUnique({
    where: { id: id },
    include: { table: true },
  });
};

const createReservations = async (reservation) => {
  return await client.reservation.create({
    data: reservation,
  });
};

const updateReservations = async (id, reservation) => {
  return await client.reservation.update({
    where: {
      id: Number(id),
    },
    data: reservation,
  });
};

const deleteReservations = async (id) => {
  return await client.reservation.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllReservations,
  getReservationsById,
  createReservations,
  updateReservations,
  deleteReservations,
};
