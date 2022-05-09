const router = require('express').Router();
const { verifyToken } = require('../helpers/helpers');
const authService = require('../services/auth.service');

router.post('/login', async (req, res) => {
  try {
    const loggedUser = await authService.loginUser(req.body);
    res.status(loggedUser.status).json(loggedUser.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/current', verifyToken, async (req, res) => {
  try {
    const token = req.headers.authorization.split(' ')[1];
    const loggedUser = await authService.getCurrentUser(token);
    res.status(loggedUser.status).json(loggedUser.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.post('/register', async (req, res) => {
  try {
    const loggedUser = await authService.registerUser(req.body);
    res.status(loggedUser.status).json(loggedUser.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
