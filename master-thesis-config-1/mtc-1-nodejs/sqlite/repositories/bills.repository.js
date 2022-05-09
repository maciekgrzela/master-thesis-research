const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllBills = async () => {
  return await client.bill.findMany({
    include: { customer: true, order: true, orderedCourses: true },
  });
};

const getBillById = async (id) => {
  return await client.bill.findUnique({
    where: { id: id },
  });
};

const createBill = async (bill) => {
  return await client.bill.create({
    data: bill,
  });
};

const updateBill = async (id, bill) => {
  return await client.bill.update({
    where: {
      id: Number(id),
    },
    data: bill,
  });
};

const deleteBill = async (id) => {
  return await client.bill.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllBills,
  getBillById,
  createBill,
  updateBill,
  deleteBill,
};
