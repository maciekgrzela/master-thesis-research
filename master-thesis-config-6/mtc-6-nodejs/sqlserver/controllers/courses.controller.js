const router = require('express').Router();
const coursesService = require('../services/courses.service');
const { verifyToken } = require('../helpers/helpers');

router.get('/', verifyToken, async (req, res, next) => {
  try {
    const entities = await coursesService.getAll();
    res.status(entities.status).json(entities.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entity = await coursesService.getById(id);
    res.status(entity.status).json(entity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id/ingredients', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const entities = await coursesService.getIngredientsForCourseId(id);
    res.status(entities.status).json(entities.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.post('/', verifyToken, async (req, res, next) => {
  try {
    const savedEntity = await coursesService.create(req.body);
    res.status(savedEntity.status).json(savedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.put('/:id', async (req, res, next) => {
  let elapsed = 0;
  const start = performance.now();

  try {
    const updatedEntity = await coursesService.update(id, req.body);
    elapsed = performance.now() - start;
    res.status(updatedEntity.status).json([elapsed]);
  } catch (e) {
    elapsed = performance.now() - start;
    res.status(e.statusCode).json([elapsed]);
  }
});

router.delete('/:id', verifyToken, async (req, res, next) => {
  try {
    const deletedEntity = await coursesService.deleteById(id);
    res.status(deletedEntity.status).json(deletedEntity.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
