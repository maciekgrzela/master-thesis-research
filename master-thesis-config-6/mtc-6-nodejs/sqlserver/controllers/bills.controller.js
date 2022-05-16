const router = require('express').Router();
const billsService = require('../services/bills.service');
const { verifyToken } = require('../helpers/helpers');

router.get('/', verifyToken, async (req, res, next) => {
  try {
    const bills = await billsService.getAll();
    res.status(bills.status).json(bills.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const bill = await billsService.getById(id);
    res.status(bill.status).json(bill.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.get('/:id/ordered-courses', verifyToken, async (req, res, next) => {
  const { id } = req.params;
  try {
    const bill = await billsService.getOrderedCoursesForBillId(id);
    res.status(bill.status).json(bill.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.post('/', verifyToken, async (req, res, next) => {
  try {
    const savedBill = await billsService.create(req.body);
    res.status(savedBill.status).json(savedBill.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.put('/:id', verifyToken, async (req, res, next) => {
  try {
    const updatedBill = await billsService.update(id, req.body);
    res.status(updatedBill.status).json(updatedBill.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

router.delete('/:id', verifyToken, async (req, res, next) => {
  try {
    const deletedBill = await billsService.deleteById(id);
    res.status(deletedBill.status).json(deletedBill.content);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

module.exports = router;
