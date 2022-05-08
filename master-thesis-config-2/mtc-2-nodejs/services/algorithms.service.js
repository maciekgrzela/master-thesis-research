const BusinessLogicError = require('../errors/BusinessLogicError');
const Road = require('../algorithms/road-plan/logic/road');
const roadPlanConfig = require('../algorithms/road-plan/helpers/roadPlanConfig');
const Coordinate = require('../algorithms/road-plan/logic/coordinate');
const Population = require('../algorithms/road-plan/logic/population');
const Status = require('../helpers/responseStatus');

const roadPlan = async (getCoordinatesDto, bestResult) => {
  try {
    roadPlanConfig.mutationProbability = 0.01;
    roadPlanConfig.populationSize = Math.floor(0.75 * getCoordinatesDto.length);
    roadPlanConfig.numberOfCoordinates = getCoordinatesDto.length;
    roadPlanConfig.numberOfDominantsInNextGeneration = Math.floor(
      0.25 * getCoordinatesDto.length
    );

    let bestRoad = undefined;
    let bestCoordinates = getCoordinatesDto.map((dto) => ({
      latitude: dto.latitude,
      longitude: dto.longitude,
      address: dto.address,
    }));

    let coordinates = getCoordinatesDto.map(
      (coord) => new Coordinate(coord.latitude, coord.longitude, coord.address)
    );

    const startSolution = new Road(coordinates);
    let population = Population.randomized(
      startSolution,
      roadPlanConfig.populationSize
    );

    let better = true;
    let iterations = 0;

    let timeout = false;

    const start = performance.now();
    while (!timeout) {
      if (better) {
        bestRoad = population.findBest();
      }

      better = false;
      let oldFit = population.maxFitness;
      population = population.evolve();

      if (population.maxFitness > oldFit) {
        better = true;
      }

      iterations++;
      const duration = performance.now() - start;
      if (duration >= 60000) {
        timeout = true;
      }
    }

    if (bestRoad === undefined) {
      throw new BusinessLogicError(
        Status.INTERNAL_ERROR,
        'Could not determine the most optimal road'
      );
    }

    return {
      status: Status.OK,
      content: {
        iterations: iterations,
        resultToBestRatio: bestResult / bestRoad.distance,
      },
    };
  } catch (e) {
    console.log(e);
    throw new BusinessLogicError(Status.INTERNAL_ERROR, e.content);
  }
};

module.exports = {
  roadPlan,
};
