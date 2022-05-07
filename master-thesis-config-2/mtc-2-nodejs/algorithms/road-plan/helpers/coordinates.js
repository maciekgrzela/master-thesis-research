const getDistanceBetweenCoordinates = (coord1, coord2) =>
  Math.sqrt(
    Math.pow(coord2.latitude - coord1.latitude, 2) +
      Math.pow(coord2.longitude - coord1.longitude, 2)
  );

const prepareBestRoadCoords = (bestRoad) => {
  let bestCoordinates = [];

  let i = 1;
  if (bestRoad === null) {
    return bestCoordinates;
  }

  bestRoad.coordinates.forEach((coord) => {
    bestCoordinates.push({
      latitude: coord.latitude,
      longitude: coord.longitude,
      address: coord.address,
      order: i,
    });
    i++;
  });

  return bestCoordinates;
};

module.exports = {
  prepareBestRoadCoords,
  getDistanceBetweenCoordinates,
};
