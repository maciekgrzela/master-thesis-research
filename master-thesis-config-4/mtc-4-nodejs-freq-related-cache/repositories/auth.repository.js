const { getPrismaClient } = require('./base.repository');

const client = getPrismaClient();

const getByUserName = async (userName) => {
  return await client.user.findFirst({
    where: { userName: userName },
    include: { UserRole: true },
  });
};

const getById = async (id) => {
  return await client.user.findFirst({
    where: { id: id },
    include: { UserRole: true },
  });
};

const createUser = async (credentials, passwordHash) => {
  return await client.user.create({
    data: {
      firstName: credentials.firstName,
      lastName: credentials.lastName,
      passwordHash: passwordHash,
      userName: credentials.userName,
      email: `${credentials.userName}@mail.com`,
      phoneNumber: '',
      normalizedUserName: credentials.userName.toUpperCase(),
      normalizedEmail: `${credentials.userName}@mail.com`.toUpperCase(),
    },
  });
};

const createUserRole = async (userId, roleId) => {
  return await client.userRole.create({
    data: {
      roleId: roleId,
      userId: userId,
    },
  });
};

const getUserRole = async (userId) => {
  return await client.userRole.findFirst({
    where: { userId: userId },
    include: { role: true },
  });
};

const getUserRoleByName = async (roleName) => {
  return await client.role.findFirst({
    where: { name: roleName },
  });
};

module.exports = {
  getByUserName,
  getUserRoleByName,
  getUserRole,
  getById,
  createUser,
  createUserRole,
};
