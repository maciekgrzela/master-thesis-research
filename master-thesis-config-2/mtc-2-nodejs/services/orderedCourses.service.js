const orderedCoursesRepository = require('../repositories/orderedCourses.repository');
const statusEntriesRepository = require('../repositories/statusEntries.repository');
const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');

const getAll = async () => {
  try {
    const courses = await orderedCoursesRepository.getAllOrderedCourses();

    return {
      status: Status.OK,
      content: courses,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingOrderedCourse =
    await orderedCoursesRepository.getOrderedCourseById(id);

  if (existingOrderedCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve orderedCourse with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingOrderedCourse,
  };
};

const create = async (course) => {
  course.statusEntries = [];

  const existingCourse = await coursesRepository.getCourseById(course.courseId);

  if (existingCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve course with specified identifier'
    );
  }

  course.course = existingCourse;

  const existingOrder = await ordersRepository.getOrderById(course.orderId);

  if (existingOrder === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to create bill. Order with specified identifier does not exist'
    );
  }

  course.order = existingOrder;

  if (course.billId !== null) {
    const existingBill = await billsRepository.getBillById(course.billId);

    if (existingBill === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to retrieve bill with specified identifier'
      );
    }

    course.bill = existingBill;
  }

  const savedOrderedCourse = await orderedCoursesRepository.createOrderedCourse(
    course
  );

  if (Object.keys(savedOrderedCourse) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to create ordered course'
    );
  }

  return {
    status: Status.CREATED,
    content: savedOrderedCourse,
  };
};

const update = async (id, course) => {
  const existingCourse = await coursesRepository.getCourseById(course.courseId);

  if (existingCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve course with specified identifier'
    );
  }

  course.course = existingCourse;

  const existingOrder = await ordersRepository.getOrderById(course.orderId);

  if (existingOrder === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to create bill. Order with specified identifier does not exist'
    );
  }

  course.order = existingOrder;

  if (course.billId !== null) {
    const existingBill = await billsRepository.getBillById(course.billId);

    if (existingBill === null) {
      throw new BusinessLogicError(
        Status.NOT_FOUND,
        'Unable to retrieve bill with specified identifier'
      );
    }

    course.bill = existingBill;
  }

  const updatedOrderedCourse =
    await orderedCoursesRepository.updateOrderedCourse(id, course);

  if (Object.keys(updatedOrderedCourse) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update ordered course'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const updateStatus = async (id, statusName) => {
  const existingOrderedCourse =
    await orderedCoursesRepository.getOrderedCourseById(id);

  if (existingOrderedCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve orderedCourse with specified identifier'
    );
  }

  const existingOrder = await ordersRepository.getOrderById(
    existingOrderedCourse.orderId
  );

  if (existingOrder === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to create bill. Order with specified identifier does not exist'
    );
  }

  const orderedStatusForOrder = await statusesRepository.getStatusByName(
    statusName
  );

  if (orderedStatusForOrder === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to update course. Status with specified identifier does not exist'
    );
  }

  const statusEntry = {
    statusId: orderedStatusForOrder.id,
    status: orderedStatusForOrder,
    note: '',
    orderId: existingOrder.id,
    order: existingOrder,
    orderedCourseId: existingOrderedCourse.id,
    orderedCourse: existingOrderedCourse,
  };

  const createdStatusEntry = await statusEntriesRepository.createStatusEntries(
    statusEntry
  );

  if (Object.keys(createdStatusEntry) === 0) {
    throw new BusinessLogicError(
      Status.INTERNAL_ERROR,
      'Unable to update ordered course'
    );
  }

  return {
    status: Status.NO_CONTENT,
    content: '',
  };
};

const deleteById = async (id) => {
  const existingOrderedCourse =
    await orderedCoursesRepository.getOrderedCourseById(id);

  if (existingOrderedCourse === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve orderedCourse with specified identifier'
    );
  }

  await orderedCoursesRepository.deleteOrderedCourse(id);

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
