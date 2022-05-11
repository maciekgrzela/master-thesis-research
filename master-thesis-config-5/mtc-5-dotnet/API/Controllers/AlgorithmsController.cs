using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Algorithms.Interfaces;
using Application.Extensions;
using Application.Resources.Coordinate.Get;
using Application.Resources.Coordinate.List;
using AutoMapper;
using Domain.Algorithms.Models;
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

        [HttpPost("road/plan")]
        public async Task<IActionResult> RoadPlanAsync([FromBody] List<GetCoordinateResource> resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var coordinates = mapper.Map<List<GetCoordinateResource>, List<Coordinate>>(resource);

            var result = await algorithmService.RoadPlan(coordinates);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            
            var resultCoordinates = mapper.Map<IEnumerable<Coordinate>, IEnumerable<CoordinateResource>>(result.Type);
            
            return Ok(resultCoordinates);
        }

    }
}