class Coordinate {
  latitude;
  longitude;
  address;

  /**
   *  Coordinate Constructor
   */
  constructor(latitude, longitude, address) {
    this.latitude = latitude;
    this.longitude = longitude;
    this.address = address;
  }

  distance = (coordinate) => {
    return Math.sqrt(
      Math.pow(coordinate.latitude - this.latitude, 2) +
        Math.pow(coordinate.longitude - this.longitude, 2)
    );
  };
}

module.exports = Coordinate;
