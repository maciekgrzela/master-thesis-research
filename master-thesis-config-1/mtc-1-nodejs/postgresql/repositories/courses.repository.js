const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getAllCourses = async () => {
  return await client.course.findMany({
    include: { coursesCategory: true, ingredients: true, orderedCourses: true },
  });
};

const getCourseById = async (id) => {
  return await client.course.findUnique({
    where: { id: id },
    include: { coursesCategory: true, ingredients: true, orderedCourses: true },
  });
};

const createCourse = async (course) => {
  return await client.course.create({
    data: course,
  });
};

const updateCourse = async (id, course) => {
  return await client.course.update({
    where: {
      id: Number(id),
    },
    data: course,
  });
};

const deleteCourse = async (id) => {
  return await client.course.delete({
    where: {
      id: Number(id),
    },
  });
};

module.exports = {
  getAllCourses,
  getCourseById,
  createCourse,
  updateCourse,
  deleteCourse,
};
