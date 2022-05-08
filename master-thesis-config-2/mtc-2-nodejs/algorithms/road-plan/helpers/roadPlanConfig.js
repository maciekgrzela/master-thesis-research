class RoadPlanConfig {
  mutationProbability;
  numberOfCoordinates;
  populationSize;
  numberOfDominantsInNextGeneration;

  constructor() {
    this.mutationProbability = 0;
    this.numberOfCoordinates = 0;
    this.populationSize = 0;
    this.numberOfDominantsInNextGeneration = 0;
  }
}

module.exports = new RoadPlanConfig();
