"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const coordinate_1 = __importDefault(require("./algorithms/road-plan/logic/coordinate"));
const population_1 = __importDefault(require("./algorithms/road-plan/logic/population"));
const road_1 = __importDefault(require("./algorithms/road-plan/logic/road"));
const roadPlanConfig_1 = __importDefault(require("./algorithms/road-plan/helpers/roadPlanConfig"));
const roadPlan = (coordinatesDto) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        roadPlanConfig_1.default.mutationProbability = 0.01;
        roadPlanConfig_1.default.populationSize = Math.floor(0.75 * coordinatesDto.length);
        roadPlanConfig_1.default.numberOfCoordinates = coordinatesDto.length;
        roadPlanConfig_1.default.numberOfDominantsInNextGeneration = Math.floor(0.25 * coordinatesDto.length);
        let bestRoad = undefined;
        let bestCoordinates = coordinatesDto;
        let coordinates = coordinatesDto.map((coord) => new coordinate_1.default(coord.latitude, coord.longitude, coord.address));
        let timeout = false;
        setTimeout(() => {
            timeout = true;
        }, 60000);
        let startSolution = new road_1.default(coordinates);
        let population = population_1.default.randomized(startSolution, roadPlanConfig_1.default.populationSize);
        let better = true;
        let iterations = 0;
        while (!timeout) {
            if (better) {
                bestRoad = population.findBest();
            }
            better = false;
            let oldFit = population.maxFitness;
            population = population.evolve();
            if (population.maxFitness > oldFit) {
                better = true;
            }
            iterations++;
        }
        bestCoordinates =
            bestRoad === undefined
                ? []
                : bestRoad.coordinates.map((coord, idx) => ({
                    address: coord.address,
                    latitude: coord.latitude,
                    longitude: coord.longitude,
                    order: idx,
                }));
        console.log(bestCoordinates.map((coord) => coord.order));
        console.log(iterations);
    }
    catch (e) {
        console.log('ERROR MESSAGE!!!!!', e.message);
        console.log('ERROR STACK!!!!!', e.stack);
    }
});
const jsonData = [
    {
        latitude: 6734,
        longitude: 1453,
        address: 'Address1',
    },
    {
        latitude: 2233,
        longitude: 10,
        address: 'Address2',
    },
    {
        latitude: 5530,
        longitude: 1424,
        address: 'Address3',
    },
    {
        latitude: 401,
        longitude: 841,
        address: 'Address4',
    },
    {
        latitude: 3082,
        longitude: 1644,
        address: 'Address5',
    },
    {
        latitude: 7608,
        longitude: 4458,
        address: 'Address6',
    },
    {
        latitude: 7573,
        longitude: 3716,
        address: 'Address7',
    },
    {
        latitude: 7265,
        longitude: 1268,
        address: 'Address8',
    },
    {
        latitude: 6898,
        longitude: 1885,
        address: 'Address9',
    },
    {
        latitude: 1112,
        longitude: 2049,
        address: 'Address10',
    },
    {
        latitude: 5468,
        longitude: 2606,
        address: 'Address11',
    },
    {
        latitude: 5989,
        longitude: 2873,
        address: 'Address12',
    },
    {
        latitude: 4706,
        longitude: 2674,
        address: 'Address13',
    },
    {
        latitude: 4612,
        longitude: 2035,
        address: 'Address14',
    },
    {
        latitude: 6347,
        longitude: 2683,
        address: 'Address15',
    },
    {
        latitude: 6107,
        longitude: 669,
        address: 'Address16',
    },
    {
        latitude: 7611,
        longitude: 5184,
        address: 'Address17',
    },
    {
        latitude: 7462,
        longitude: 3590,
        address: 'Address18',
    },
    {
        latitude: 7732,
        longitude: 4723,
        address: 'Address19',
    },
    {
        latitude: 5900,
        longitude: 3561,
        address: 'Address20',
    },
    {
        latitude: 4483,
        longitude: 3369,
        address: 'Address21',
    },
    {
        latitude: 6101,
        longitude: 1110,
        address: 'Address22',
    },
    {
        latitude: 5199,
        longitude: 2182,
        address: 'Address23',
    },
    {
        latitude: 1633,
        longitude: 2809,
        address: 'Address24',
    },
    {
        latitude: 4307,
        longitude: 2322,
        address: 'Address25',
    },
    {
        latitude: 675,
        longitude: 1006,
        address: 'Address26',
    },
    {
        latitude: 7555,
        longitude: 4819,
        address: 'Address27',
    },
    {
        latitude: 7541,
        longitude: 3981,
        address: 'Address28',
    },
    {
        latitude: 3177,
        longitude: 756,
        address: 'Address29',
    },
    {
        latitude: 7352,
        longitude: 4506,
        address: 'Address30',
    },
    {
        latitude: 7545,
        longitude: 2801,
        address: 'Address31',
    },
    {
        latitude: 3245,
        longitude: 3305,
        address: 'Address32',
    },
    {
        latitude: 6426,
        longitude: 3173,
        address: 'Address33',
    },
    {
        latitude: 4608,
        longitude: 1198,
        address: 'Address34',
    },
    {
        latitude: 23,
        longitude: 2216,
        address: 'Address35',
    },
    {
        latitude: 7248,
        longitude: 3779,
        address: 'Address36',
    },
    {
        latitude: 7762,
        longitude: 4595,
        address: 'Address37',
    },
    {
        latitude: 7392,
        longitude: 2244,
        address: 'Address38',
    },
    {
        latitude: 3484,
        longitude: 2829,
        address: 'Address39',
    },
    {
        latitude: 6271,
        longitude: 2135,
        address: 'Address40',
    },
    {
        latitude: 4985,
        longitude: 140,
        address: 'Address41',
    },
    {
        latitude: 1916,
        longitude: 1569,
        address: 'Address42',
    },
    {
        latitude: 7280,
        longitude: 4899,
        address: 'Address43',
    },
    {
        latitude: 7509,
        longitude: 3239,
        address: 'Address44',
    },
    {
        latitude: 10,
        longitude: 2676,
        address: 'Address45',
    },
    {
        latitude: 6807,
        longitude: 2993,
        address: 'Address46',
    },
    {
        latitude: 5185,
        longitude: 3258,
        address: 'Address47',
    },
    {
        latitude: 3023,
        longitude: 1942,
        address: 'Address48',
    },
];
roadPlan(jsonData);
