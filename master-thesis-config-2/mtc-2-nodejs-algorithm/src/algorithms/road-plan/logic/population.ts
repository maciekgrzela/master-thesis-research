import Road from './road';
import roadPlanConfig from '../helpers/roadPlanConfig';

class Population {
  roads: Road[];
  maxFitness: number;

  /**
   *      Population constructor
   */
  constructor(roads: Road[]) {
    this.roads = roads;
    this.maxFitness = this.calculateMaxFitness();
  }

  static randomized = (road: Road, size: number) => {
    let tmp: Road[] = [];

    for (let i = 0; i < size; ++i) {
      tmp.push(road.rearrange());
    }

    return new Population(tmp);
  };

  calculateMaxFitness = () => {
    return this.roads.sort((a, b) => b.fitnessRatio - a.fitnessRatio)[0]
      .fitnessRatio;
  };

  selection = () => {
    while (true) {
      var index = Math.floor(
        Math.random() * (roadPlanConfig.populationSize - 1 - 0 + 1) + 0
      );

      if (Math.random() < this.roads[index].fitnessRatio / this.maxFitness) {
        return new Road(this.roads[index].coordinates);
      }
    }
  };

  generateNewPopulation = (size: number) => {
    let roads: Road[] = [];

    for (let i = 0; i < size; ++i) {
      let road = this.selection().crossing(this.selection());

      road.coordinates.forEach((_) => {
        road = road.mutation();
      });

      roads.push(road);
    }

    return new Population(roads);
  };

  getEliteIndividuals = (size: number) => {
    let roads: Road[] = [];
    let tmp = new Population(this.roads);

    for (let i = 0; i < size; ++i) {
      const best = tmp.findBest();

      if (!best) {
        throw new Error('Undefined value for best variable');
      }
      roads.push(best);
      tmp = new Population(
        tmp.roads.filter(
          (p) =>
            roads.find((x) => x.fitnessRatio === p.fitnessRatio) === undefined
        )
      );
    }
    return new Population(roads);
  };

  findBest = () => {
    return this.roads.find((p) => p.fitnessRatio === this.maxFitness);
  };

  evolve = () => {
    let elite = this.getEliteIndividuals(
      roadPlanConfig.numberOfDominantsInNextGeneration
    );
    let newPopulation = this.generateNewPopulation(
      roadPlanConfig.populationSize -
        roadPlanConfig.numberOfDominantsInNextGeneration
    );
    return new Population([...elite.roads, ...newPopulation.roads]);
  };
}

export default Population;
