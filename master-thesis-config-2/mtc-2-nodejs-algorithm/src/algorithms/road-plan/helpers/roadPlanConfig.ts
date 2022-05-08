class RoadPlanConfig {
  mutationProbability: number;
  numberOfCoordinates: number;
  populationSize: number;
  numberOfDominantsInNextGeneration: number;

  constructor() {
    this.mutationProbability = 0;
    this.numberOfCoordinates = 0;
    this.populationSize = 0;
    this.numberOfDominantsInNextGeneration = 0;
  }
}

export default new RoadPlanConfig();
