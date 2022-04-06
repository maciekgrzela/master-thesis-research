const BusinessLogicError = require('../errors/BusinessLogicError');
const Status = require('../helpers/responseStatus');
const {
  getByUserName,
  getById,
  createUser,
  getUserRoleByName,
  createUserRole,
} = require('../repositories/auth.repository');
const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');

const loginUser = async (credentials) => {
  const user = await getByUserName(credentials.userName);

  if (user === null) {
    throw new BusinessLogicError(Status.UNAUTHORIZED, 'User does not exist');
  }

  const passwordIsValid = await bcrypt.compare(
    credentials.password,
    user.passwordHash
  );

  if (!passwordIsValid) {
    throw new BusinessLogicError(
      Status.UNAUTHORIZED,
      'Invalid password for user'
    );
  }

  const token = jwt.sign({ id: user.id }, global.TOKEN_SECRET, {
    expiresIn: 86400,
  });

  const userResource = {
    ...user,
    password: credentials.password,
    token: token,
  };

  return {
    status: Status.OK,
    content: userResource,
  };
};

const registerUser = async (credentials) => {
  const hashedPassword = await bcrypt.hash(credentials.password, 8);

  const existingUser = await getByUserName(credentials.userName);

  if (existingUser !== null) {
    throw new BusinessLogicError(
      Status.CONFLICT,
      `User with username ${credentials.userName} already exist`
    );
  }

  const existingRole = await getUserRoleByName(credentials.role);

  if (existingRole === null) {
    throw new BusinessLogicError(Status.CONFLICT, `User role does not exist`);
  }

  const user = await createUser(credentials, hashedPassword);

  await createUserRole(user.id, existingRole.id);

  const token = jwt.sign({ id: user.id }, global.TOKEN_SECRET, {
    expiresIn: 86400,
  });

  const userWithRole = await getById(user.id);

  const userResource = {
    ...userWithRole,
    password: credentials.password,
    token: token,
  };

  return {
    status: Status.CREATED,
    content: userResource,
  };
};

const getCurrentUser = async (token) => {
  if (!token) {
    throw new BusinessLogicError(
      Status.UNAUTHORIZED,
      'No currently logged user'
    );
  }

  let user = null;
  let decoded = null;

  try {
    decoded = jwt.verify(token, global.TOKEN_SECRET);
  } catch (e) {
    throw new BusinessLogicError(
      Status.UNAUTHORIZED,
      'Invalid auth token structure'
    );
  }

  user = await getById(decoded.id);

  if (user === null) {
    throw new BusinessLogicError(Status.UNAUTHORIZED, 'User does not exist');
  }

  const userResource = {
    ...user,
    token: token,
  };

  return {
    status: Status.OK,
    content: userResource,
  };
};

module.exports = {
  loginUser,
  registerUser,
  getCurrentUser,
};
