import Coordinate from './coordinate';
import { CoordinateDto } from '../models/coordinateDto';
import roadPlanConfig from '../helpers/roadPlanConfig';

class Road {
  coordinates: Coordinate[];
  distance: number = 0;
  fitnessRatio: number;

  /**
   *  Road Constructor
   */
  constructor(coordinates: Coordinate[]) {
    this.coordinates = coordinates;
    this.distance = this.calculateDistance();
    this.fitnessRatio = this.calculateFitnessRatio();
  }

  rearrange = () => {
    let temp: Coordinate[] = [...this.coordinates];
    let count = temp.length;

    while (count > 1) {
      count--;
      let randomElementIndex = Math.floor(Math.random() * (count - 0 + 1) + 0);
      let randomElement = temp[randomElementIndex];
      temp[randomElementIndex] = temp[count];
      temp[count] = randomElement;
    }

    return new Road(temp);
  };

  calculateDistance = () => {
    let totalDistance = 0;
    for (let i = 0; i < this.coordinates.length; i++) {
      const nextItemIndex = (i + 1) % this.coordinates.length;
      totalDistance += this.coordinates[i].distance(
        this.coordinates[nextItemIndex]
      );
    }

    return totalDistance;
  };

  calculateFitnessRatio = () => {
    if (this.distance === 0) {
      this.distance = this.calculateDistance();
    }

    return 1.0 / this.distance;
  };

  mutation = () => {
    let coords: Coordinate[] = [...this.coordinates];
    let prob = Math.random();
    let road: null | Road = null;

    if (roadPlanConfig.mutationProbability > prob) {
      const swappedIndexOne = Math.floor(
        Math.random() * (this.coordinates.length - 1 - 0 + 1) + 0
      );

      const swappedIndexTwo = Math.floor(
        Math.random() * (this.coordinates.length - 1 - 0 + 1) + 0
      );

      let temp = coords[swappedIndexOne];
      coords[swappedIndexOne] = coords[swappedIndexTwo];
      coords[swappedIndexTwo] = temp;
    }

    road = new Road(coords);

    return road;
  };

  crossing = (road: Road) => {
    const firstPoint = Math.floor(
      Math.random() * (road.coordinates.length - 1 - 0 + 1) + 0
    );

    const secondPoint = Math.floor(
      Math.random() * (road.coordinates.length - 1 - firstPoint + 1) +
        firstPoint
    );

    let returnedRoad: null | Road = null;

    let firstRange = this.coordinates.slice(
      firstPoint,
      firstPoint + (secondPoint - firstPoint + 1)
    );
    let exceptFirstRange = road.coordinates.filter(
      (p) => firstRange.find((x) => x.address === p.address) === undefined
    );
    let merged = [
      ...exceptFirstRange.filter((coord, idx) => idx <= firstPoint),
      ...firstRange,
      ...exceptFirstRange.filter((coord, idx) => idx > firstPoint),
    ];

    returnedRoad = new Road(merged);

    return returnedRoad;
  };
}

export default Road;
