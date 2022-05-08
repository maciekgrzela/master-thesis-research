using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Algorithms.Interfaces;
using Application.Extensions;
using Application.Resources.Coordinate.Get;
using AutoMapper;
using Domain.Algorithms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlgorithmsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAlgorithmService algorithmService;
        public AlgorithmsController(IMapper mapper, IAlgorithmService algorithmService)
        {
            this.algorithmService = algorithmService;
            this.mapper = mapper;
        }

        [HttpPost("road/plan/{bestResult}")]
        [AllowAnonymous]
        public async Task<IActionResult> RoadPlanAsync([FromBody] List<GetCoordinateResource> resource, double bestResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var coordinates = mapper.Map<List<GetCoordinateResource>, List<Coordinate>>(resource);

            var result = await algorithmService.RoadPlan(coordinates, bestResult);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Type);
        }

    }
}