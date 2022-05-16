const router = require('express').Router();
const ordersService = require('../services/orders.service');
const { verifyToken } = require('../helpers/helpers');

router.get('/', verifyToken, async (req, res, next) => {
  try {
    const entities = await ordersService.getAll();
    res.status(entities.status).json(entities.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id', async (req, res, next) => {
  let elapsed = 0;
  const start = performance.now();
  const { id } = req.params;
  try {
    const entity = await ordersService.getById(id);
    elapsed = performance.now() - start;
    res.status(entity.status).json([elapsed]);
  } catch (e) {
    elapsed = performance.now() - start;
    res.status(e.statusCode).json([elapsed]);
  }
});

router.get('/tables/:id/last-order', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entity = await ordersService.getLastTableOrder(id);
    res.status(entity.status).json(entity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/tables/:id/orders', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entity = await ordersService.getTableOrders(id);
    res.status(entity.status).json(entity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/user/:id/orders', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entity = await ordersService.getUserOrders(id);
    res.status(entity.status).json(entity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.post('/', verifyToken, async (req, res, next) => {
  try {
    const savedEntity = await ordersService.create(req.body);
    res.status(savedEntity.status).json(savedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.put('/:id', verifyToken, async (req, res, next) => {
  try {
    const updatedEntity = await ordersService.update(id, req.body);
    res.status(updatedEntity.status).json(updatedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
