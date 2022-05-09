const router = require('express').Router();
const reservationsService = require('../services/reservations.service');
const { verifyToken } = require('../helpers/helpers');

router.get('/', verifyToken, async (req, res, next) => {
  try {
    const entities = await reservationsService.getAll();
    res.status(entities.status).json(entities.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entity = await reservationsService.getById(id);
    res.status(entity.status).json(entity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.post('/', verifyToken, async (req, res, next) => {
  try {
    const savedEntity = await reservationsService.create(req.body);
    res.status(savedEntity.status).json(savedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.put('/:id', verifyToken, async (req, res, next) => {
  try {
    const updatedEntity = await reservationsService.update(id, req.body);
    res.status(updatedEntity.status).json(updatedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.delete('/:id', verifyToken, async (req, res, next) => {
  try {
    const deletedEntity = await reservationsService.deleteById(id);
    res.status(deletedEntity.status).json(deletedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
