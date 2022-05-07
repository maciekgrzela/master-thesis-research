const randomizePopulation = (road, n) => {
  let roads = [];

  for (let i = 0; i < n; i++) {
    roads.push(road.rearrange());
  }

  return new Population(roads);
};

module.exports = {
  randomizePopulation,
};
