const algorithmsConfig = require('../../../helpers/algorithmsConfig');

class Population {
  roads;
  maxFit;

  constructor(roads) {
    this.roads = roads;
    this.maxFit = this.calculateMaxFit();
  }

  calculateMaxFit = () => {
    return this.roads.sort((a, b) => b.fitnessRatio - a.fitnessRatio)[0];
  };

  selection = () => {
    while (true) {
      let index = Math.floor(
        Math.random() * (algorithmsConfig.populationSize - 0 + 1) + 0
      );

      if (Math.random() < this.roads[index].fitnessRatio / this.maxFit)
        return new Road(this.roads[index].coordinates);
    }
  };

  generateNewPopulation = (size) => {
    let population = [];

    for (let i = 0; i < size; ++i) {
      let road = this.selection().crossing(this.selection());

      road.coordinates.forEach((coordinate) => {
        road = road.mutation();
      });

      population.push(road);
    }

    return new Population(population);
  };

  elite = (size) => {
    let best = [];
    let tmp = new Population(this.roads);

    for (let i = 0; i < size; ++i) {
      best.push(tmp.findBest());
      let roadsExceptBest = tmp.roads.filter((p) => !best.includes(p));
      tmp = new Population(roadsExceptBest);
    }

    return new Population(best);
  };

  findBest = () => {
    this.roads.forEach((road) => {
      if (road.fitnessRatio === this.maxFit) {
        return road;
      }
    });

    return null;
  };

  evolve = () => {
    const best = this.elite(algorithmsConfig.numberOfDominantsInNextGeneration);
    const newPopulation = this.generateNewPopulation(
      algorithmsConfig.populationSize -
        algorithmsConfig.numberOfDominantsInNextGeneration
    );

    return new Population([...best.roads, ...newPopulation.roads]);
  };
}

module.exports = Population;
