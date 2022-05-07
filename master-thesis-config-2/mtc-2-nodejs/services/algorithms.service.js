const BusinessLogicError = require('../errors/BusinessLogicError');
const AlgorithmsConfig = require('../helpers/algorithmsConfig');
const Road = require('../algorithms/road-plan/logic/road');
const {
  randomizePopulation,
} = require('../algorithms/road-plan/helpers/population');
const {
  prepareBestRoadCoords,
} = require('../algorithms/road-plan/helpers/coordinates');

const roadPlan = async (coordinates) => {
  try {
    AlgorithmsConfig.mutationProbability = 0.01;
    AlgorithmsConfig.populationSize = Math.floor(0.75 * coordinates.length);
    AlgorithmsConfig.numberOfCoordinates = coordinates.length;
    AlgorithmsConfig.numberOfDominantsInNextGeneration = Math.floor(
      0.25 * coordinates.length
    );

    let bestRoad = null;
    let bestCoordinates = [];
    let timeout = false;

    setTimeout(
      (timeout) => {
        timeout = true;
      },
      60000,
      timeout
    );

    const startSolution = new Road(coordinates);
    let population = randomizePopulation(
      startSolution,
      AlgorithmsConfig.populationSize
    );
    let better = true;

    while (!timeout) {
      if (better) {
        const best = population.findBest();
        bestRoad = best;
      }

      better = false;
      const oldFit = population.maxFit;

      population = population.evolve();

      if (population.maxFit > oldFit) {
        better = true;
      }
    }

    bestCoordinates = prepareBestRoadCoords(bestRoad);

    return {
      status: Status.OK,
      content: bestCoordinates,
    };
  } catch (e) {
    throw new BusinessLogicError(e.statusCode, e.content);
  }
};

const getById = async (id) => {
  const existingBill = await billsRepository.getBillById(id);

  if (existingBill === null) {
    throw new BusinessLogicError(
      Status.NOT_FOUND,
      'Unable to retrieve bill with specified identifier'
    );
  }

  return {
    status: Status.OK,
    content: existingBill,
  };
};

module.exports = {
  roadPlan,
};
