const router = require('express').Router();
const tablesService = require('../services/tables.service');
const { verifyToken } = require('../helpers/helpers');

router.get('/', verifyToken, async (req, res, next) => {
  try {
    const entities = await tablesService.getAll();
    res.status(entities.status).json(entities.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entity = await tablesService.getById(id);
    res.status(entity.status).json(entity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.post('/', verifyToken, async (req, res, next) => {
  try {
    const savedEntity = await tablesService.create(req.body);
    res.status(savedEntity.status).json(savedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.put('/:id', verifyToken, async (req, res, next) => {
  try {
    const updatedEntity = await tablesService.update(id, req.body);
    res.status(updatedEntity.status).json(updatedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.delete('/:id', verifyToken, async (req, res, next) => {
  try {
    const deletedEntity = await tablesService.deleteById(id);
    res.status(deletedEntity.status).json(deletedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
