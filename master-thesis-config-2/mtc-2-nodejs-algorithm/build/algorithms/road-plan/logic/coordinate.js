"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Coordinate {
    /**
     *  Coordinate Constructor
     */
    constructor(latitude, longitude, address) {
        this.distance = (coordinate) => {
            return Math.sqrt(Math.pow(coordinate.latitude - this.latitude, 2) +
                Math.pow(coordinate.longitude - this.longitude, 2));
        };
        this.latitude = latitude;
        this.longitude = longitude;
        this.address = address;
    }
}
exports.default = Coordinate;
