const router = require('express').Router();
const { verifyToken } = require('../helpers/helpers');
const algorithmsService = require('../services/algorithms.service');

router.get('/road/plan', verifyToken, async (req, res, next) => {
  try {
    const coordinates = req.body;
    const result = await algorithmsService.roadPlan(coordinates);
    res.status(result.status).json(result.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
