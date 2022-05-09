const swaggerJSDoc = require('swagger-jsdoc');
const swaggerUI = require('swagger-ui-express');

const swaggerDefinition = {
  openapi: '3.0.0',
  info: {
    title: 'Express API for JSONPlaceholder',
    version: '1.0.0',
    description:
      'This is a REST API application made with Express. It retrieves data from JSONPlaceholder.',
    license: {
      name: 'Licensed Under MIT',
      url: 'https://spdx.org/licenses/MIT.html',
    },
    contact: {
      name: 'JSONPlaceholder',
      url: 'https://jsonplaceholder.typicode.com',
    },
  },
  servers: [
    {
      url: 'http://localhost:3000',
      description: 'Development server',
    },
  ],
};

const options = {
  swaggerDefinition,
  apis: ['./controllers/*.js'],
};

const swaggerSpec = swaggerJSDoc(options);

const express = require('express');
const createError = require('http-errors');
const morgan = require('morgan');
const crypto = require('crypto');
require('dotenv').config();

const app = express();

global.__basedir = __dirname;
global.TOKEN_SECRET = crypto.randomBytes(32).toString('hex');

app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(morgan('dev'));

app.use('/api/Auth', require('./controllers/auth.controller'));
app.use('/api/bills', require('./controllers/bills.controller'));
app.use(
  '/api/course-categories',
  require('./controllers/courseCategories.controller')
);
app.use('/api/courses', require('./controllers/courses.controller'));
app.use('/api/customers', require('./controllers/customers.controller'));
app.use('/api/halls', require('./controllers/halls.controller'));
app.use('/api/ingredients', require('./controllers/ingredients.controller'));
app.use(
  '/api/ordered-courses',
  require('./controllers/orderedCourses.controller')
);
app.use(
  '/api/product-categories',
  require('./controllers/productCategories.controller')
);
app.use('/api/products', require('./controllers/products.controller'));
app.use('/api/reservations', require('./controllers/reservations.controller'));
app.use('/api/statuses', require('./controllers/statuses.controller'));
app.use('/api/tables', require('./controllers/tables.controller'));

app.use('/api-docs', swaggerUI.serve, swaggerUI.setup(swaggerSpec));

app.use((req, res, next) => {
  next(createError.NotFound());
});

app.use((err, req, res, next) => {
  res.status(err.status || 500);
  res.send({
    status: err.status || 500,
    message: err.message,
  });
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`🚀 @ http://localhost:${PORT}`));
