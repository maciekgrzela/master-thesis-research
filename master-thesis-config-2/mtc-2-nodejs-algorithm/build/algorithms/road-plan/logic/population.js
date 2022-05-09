"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const road_1 = __importDefault(require("./road"));
const roadPlanConfig_1 = __importDefault(require("../helpers/roadPlanConfig"));
class Population {
    /**
     *      Population constructor
     */
    constructor(roads) {
        this.calculateMaxFitness = () => {
            return this.roads.sort((a, b) => b.fitnessRatio - a.fitnessRatio)[0]
                .fitnessRatio;
        };
        this.selection = () => {
            while (true) {
                var index = Math.floor(Math.random() * (roadPlanConfig_1.default.populationSize - 1 - 0 + 1) + 0);
                if (Math.random() < this.roads[index].fitnessRatio / this.maxFitness) {
                    return new road_1.default(this.roads[index].coordinates);
                }
            }
        };
        this.generateNewPopulation = (size) => {
            let roads = [];
            for (let i = 0; i < size; ++i) {
                let road = this.selection().crossing(this.selection());
                road.coordinates.forEach((_) => {
                    road = road.mutation();
                });
                roads.push(road);
            }
            return new Population(roads);
        };
        this.getEliteIndividuals = (size) => {
            let roads = [];
            let tmp = new Population(this.roads);
            for (let i = 0; i < size; ++i) {
                const best = tmp.findBest();
                if (!best) {
                    throw new Error('Undefined value for best variable');
                }
                roads.push(best);
                tmp = new Population(tmp.roads.filter((p) => roads.find((x) => x.fitnessRatio === p.fitnessRatio) === undefined));
            }
            return new Population(roads);
        };
        this.findBest = () => {
            return this.roads.find((p) => p.fitnessRatio === this.maxFitness);
        };
        this.evolve = () => {
            let elite = this.getEliteIndividuals(roadPlanConfig_1.default.numberOfDominantsInNextGeneration);
            let newPopulation = this.generateNewPopulation(roadPlanConfig_1.default.populationSize -
                roadPlanConfig_1.default.numberOfDominantsInNextGeneration);
            return new Population([...elite.roads, ...newPopulation.roads]);
        };
        this.roads = roads;
        this.maxFitness = this.calculateMaxFitness();
    }
}
Population.randomized = (road, size) => {
    let tmp = [];
    for (let i = 0; i < size; ++i) {
        tmp.push(road.rearrange());
    }
    return new Population(tmp);
};
exports.default = Population;
