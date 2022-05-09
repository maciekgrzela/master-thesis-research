const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllTables = async () => {
  return await client.table.findMany({
    include: { hall: true, orders: true, reservations: true },
  });
};

const getTablesById = async (id) => {
  return await client.table.findUnique({
    where: { id: id },
    include: { hall: true, orders: true, reservations: true },
  });
};

const createTables = async (table) => {
  return await client.table.create({
    data: table,
  });
};

const updateTables = async (id, table) => {
  return await client.table.update({
    where: {
      id: Number(id),
    },
    data: table,
  });
};

const deleteTables = async (id) => {
  return await client.table.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllTables,
  getTablesById,
  createTables,
  updateTables,
  deleteTables,
};
