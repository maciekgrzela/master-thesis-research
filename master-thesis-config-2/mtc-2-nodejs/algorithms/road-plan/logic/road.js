const algorithmsConfig = require('../../../helpers/algorithmsConfig');
const { getDistanceBetweenCoordinates } = require('../helpers/coordinates');

class Road {
  coordinates;
  fitnessRatio;
  distance;

  constructor(coordinates) {
    this.coordinates = coordinates;
    this.distance = this.calculateDistance();
    this.fitnessRatio = this.calculateFitnessRatio();
  }

  calculateDistance = () => {
    let totalDistance = 0.0;

    this.coordinates.forEach((coordinate) => {
      const nextItemIndex = (i + 1) % this.coordinates.length;
      totalDistance += getDistanceBetweenCoordinates(
        coordinate,
        this.coordinates[nextItemIndex]
      );
    });

    return totalDistance;
  };

  calculateFitnessRatio = () => {
    if (this.distance === 0) {
      this.distance = this.calculateDistance();
    }

    return 1 / this.distance;
  };

  mutation = () => {
    const coords = [...this.coordinates];
    const probability = Math.random();
    let road = undefined;

    if (algorithmsConfig.mutationProbability > probability) {
      const swappedIndexOne = Math.floor(
        Math.random() * (this.coordinates.length - 0 + 1) + 0
      );
      const swappedIndexTwo = Math.floor(
        Math.random() * (this.coordinates.length - 0 + 1) + 0
      );

      let temp = coords[swappedIndexOne];
      coords[swappedIndexOne] = coords[swappedIndexTwo];
      coords[swappedIndexTwo] = temp;
    }

    road = new Road(coords);

    return road;
  };

  crossing = (road) => {
    const firstPoint = Math.floor(
      Math.random() * (road.coordinates.length - 0 + 1) + 0
    );
    const secondPoint = Math.floor(
      Math.random() * (this.coordinates.length - firstPoint + 1) + firstPoint
    );

    let returnedRoad = undefined;

    let firstRange = this.coordinates.slice(
      firstPoint,
      firstPoint + (secondPoint - firstPoint + 1)
    );
    let exceptFirstRange = this.coordinates.filter(
      (coord) => !firstRange.includes(coord)
    );
    let merged = [
      ...exceptFirstRange.slice(0, firstPoint),
      ...firstRange,
      ...exceptFirstRange.slice(firstPoint + 1),
    ];

    returnedRoad = new Road(merged);

    return returnedRoad;
  };

  rearrange = () => {
    const temp = [...this.coordinates];
    let counter = temp.length;

    while (counter > 1) {
      counter--;
      const randomIndex = Math.floor(Math.random() * (counter + 1 - 0 + 1) + 0);
      let coord = temp[randomIndex];
      temp[randomIndex] = temp[counter];
      temp[counter] = coord;
    }

    return new Road(temp);
  };
}

module.exports = Road;
