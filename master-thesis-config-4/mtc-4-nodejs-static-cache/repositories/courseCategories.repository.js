const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllCourseCategories = async () => {
  return await client.coursesCategory.findMany({});
};

const getCourseCategoryById = async (id) => {
  return await client.coursesCategory.findUnique({
    where: { id: id },
  });
};

const createCourseCategory = async (bill) => {
  return await client.coursesCategory.create({
    data: bill,
  });
};

const updateCourseCategory = async (id, bill) => {
  return await client.coursesCategory.update({
    where: {
      id: Number(id),
    },
    data: bill,
  });
};

const deleteCourseCategory = async (id) => {
  return await client.coursesCategory.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllCourseCategories,
  getCourseCategoryById,
  createCourseCategory,
  updateCourseCategory,
  deleteCourseCategory,
};
