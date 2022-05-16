const router = require('express').Router();
const productsService = require('../services/products.service');
const { verifyToken } = require('../helpers/helpers');

router.get('/', verifyToken, async (req, res, next) => {
  try {
    const entities = await productsService.getAll();
    res.status(entities.status).json(entities.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entity = await productsService.getById(id);
    res.status(entity.status).json(entity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.post('/', async (req, res, next) => {
  let elapsed = 0;
  const start = performance.now();

  try {
    const savedEntity = await productsService.create(req.body);
    elapsed = performance.now() - start;
    res.status(savedEntity.status).json([elapsed]);
  } catch (e) {
    elapsed = performance.now() - start;
    res.status(e.statusCode).json([elapsed]);
  }
});

router.put('/:id', verifyToken, async (req, res, next) => {
  try {
    const updatedEntity = await productsService.update(id, req.body);
    res.status(updatedEntity.status).json(updatedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
