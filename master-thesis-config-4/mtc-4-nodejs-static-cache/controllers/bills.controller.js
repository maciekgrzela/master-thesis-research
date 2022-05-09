const router = require('express').Router();
const billsService = require('../services/bills.service');
const { verifyToken } = require('../helpers/helpers');
const {
  staticCacheMiddleware,
  cacheResponseMiddleware,
  invalidateCacheMiddleware,
} = require('../cache/cacheMiddlewares');

router.get(
  '/',
  async (req, res, next) => {
    await staticCacheMiddleware(req, res, next);
  },
  async (req, res, next) => {
    try {
      const { pageSize, pageNumber } = req.query;
      const bills = await billsService.getAll(pageSize, pageNumber);
      res.locals.content = bills.content;
      res.locals.status = bills.status;
      next();
    } catch (e) {
      console.log(e);
      res.status(e.statusCode).json(e.message);
    }
  },
  async (req, res, next) => {
    await cacheResponseMiddleware(req, res, next);
  }
);

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

router.post(
  '/',
  // verifyToken,
  async (req, res, next) => {
    try {
      const savedBill = await billsService.create(req.body);
      res.locals.content = savedBill.content;
      res.locals.status = savedBill.status;
      res.locals.entity = 'bills';
      next();
    } catch (e) {
      console.log(e);
      res.status(e.statusCode).json(e.message);
    }
  },
  invalidateCacheMiddleware
);

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
