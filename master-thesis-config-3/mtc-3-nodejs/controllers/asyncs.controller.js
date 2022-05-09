const router = require('express').Router();
const axios = require('axios').default;

router.get('/', async (req, res, next) => {
  try {
    const baseAddress = 'http://localhost:9000';

    axios.defaults.baseURL = baseAddress;
    axios.defaults.headers.get['Content-Type'] = 'application/json';

    const entitiesAmount = [100, 200, 500, 1000, 2000];
    let resultInstances = [];

    for (let i = 0; i < 30; i++) {
      entitiesAmount.forEach((amount) => {
        let resultInstance = {
          entitiesAmount: amount,
        };

        const start = performance.now();

        axios
          .get(`/random/entities/${amount}`)
          .then((res) => {
            const duration = performance.now() - start;
            resultInstance.executionTime = duration;
            resultInstance.isError = false;
            resultInstances.push(resultInstance);
          })
          .catch((error) => {
            const duration = performance.now() - start;
            resultInstance.executionTime = duration;
            resultInstance.isError = true;
            resultInstances.push(resultInstance);
          });
      });
    }

    const result = {
      averageTimeFor100Entities: average(
        resultInstances.filter((p) => p.entitiesAmount === 100)
      ),
      percentageErrorFor100Entities: percentage(
        resultInstances.filter((p) => p.entitiesAmount === 100 && p.isError),
        resultInstances.filter((p) => p.entitiesAmount === 100)
      ),
      averageTimeFor200Entities: average(
        resultInstances.filter((p) => p.entitiesAmount === 200)
      ),
      percentageErrorFor200Entities: percentage(
        resultInstances.filter((p) => p.entitiesAmount === 200 && p.isError),
        resultInstances.filter((p) => p.entitiesAmount === 200)
      ),
      averageTimeFor500Entities: average(
        resultInstances.filter((p) => p.entitiesAmount === 500)
      ),
      percentageErrorFor500Entities: percentage(
        resultInstances.filter((p) => p.entitiesAmount === 500 && p.isError),
        resultInstances.filter((p) => p.entitiesAmount === 500)
      ),
      averageTimeFor1000Entities: average(
        resultInstances.filter((p) => p.entitiesAmount === 1000)
      ),
      percentageErrorFor1000Entities: percentage(
        resultInstances.filter((p) => p.entitiesAmount === 1000 && p.isError),
        resultInstances.filter((p) => p.entitiesAmount === 1000)
      ),
      averageTimeFor2000Entities: average(
        resultInstances.filter((p) => p.entitiesAmount === 2000)
      ),
      percentageErrorFor2000Entities: percentage(
        resultInstances.filter((p) => p.entitiesAmount === 2000 && p.isError),
        resultInstances.filter((p) => p.entitiesAmount === 2000)
      ),
      averageTimeFor5000Entities: average(
        resultInstances.filter((p) => p.entitiesAmount === 5000)
      ),
      percentageErrorFor5000Entities: percentage(
        resultInstances.filter((p) => p.entitiesAmount === 5000 && p.isError),
        resultInstances.filter((p) => p.entitiesAmount === 5000)
      ),
    };

    res.status(200).json(result);
  } catch (e) {
    res.status(e.statusCode).json(e.message);
  }
});

const average = (arr) => {
  return arr.reduce((a, b) => a + b, 0) / arr.length;
};

const percentage = (arr1, arr2) => {
  return arr1.reduce((a, b) => a + b, 0) / arr2.length;
};

module.exports = router;
