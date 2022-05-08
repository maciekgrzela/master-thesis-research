class Coordinate {
  latitude: number;
  longitude: number;
  address: string;

  /**
   *  Coordinate Constructor
   */
  constructor(latitude: number, longitude: number, address: string) {
    this.latitude = latitude;
    this.longitude = longitude;
    this.address = address;
  }

  distance = (coordinate: Coordinate) => {
    return Math.sqrt(
      Math.pow(coordinate.latitude - this.latitude, 2) +
        Math.pow(coordinate.longitude - this.longitude, 2)
    );
  };
}

export default Coordinate;
