const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllOrderedCourses = async () => {
  return await client.orderedCourse.findMany({
    include: { bill: true },
  });
};

const getOrderedCourseById = async (id) => {
  return await client.orderedCourse.findUnique({
    where: { id: id },
    include: { bill: true },
  });
};

const createOrderedCourse = async (orderedCourse) => {
  return await client.orderedCourse.create({
    data: orderedCourse,
  });
};

const updateOrderedCourse = async (id, orderedCourse) => {
  return await client.orderedCourse.update({
    where: {
      id: Number(id),
    },
    data: orderedCourse,
  });
};

const deleteOrderedCourse = async (id) => {
  return await client.orderedCourse.delete({
    where: {
      id: Number(id),
    },
  });
};

const getOrderedCoursesForBillId = async (id) => {
  return await client.orderedCourse.findMany({
    include: { bill: true },
    where: { billId: id },
  });
};

module.exports = {
  getAllOrderedCourses,
  getOrderedCourseById,
  createOrderedCourse,
  updateOrderedCourse,
  deleteOrderedCourse,
  getOrderedCoursesForBillId,
};
