const { PrismaClient } = require('@prisma/client');

const client = new PrismaClient({
  rejectOnNotFound: false,
});

const getPrismaClient = () => {
  return client;
};

module.exports = {
  getPrismaClient,
};
