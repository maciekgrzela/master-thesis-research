const router = require('express').Router();
const algorithmsService = require('../services/algorithms.service');

router.post('/road/plan/:bestResult', async (req, res, next) => {
  try {
    const coordinates = req.body;
    const { bestResult } = req.params;
    const result = await algorithmsService.roadPlan(coordinates, bestResult);
    res.status(result.status).json(result.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
