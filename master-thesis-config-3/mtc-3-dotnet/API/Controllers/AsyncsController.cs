using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Application;
using Application.Extensions;
using Application.Params;
using Application.Resources.Bills.Get;
using Application.Resources.Bills.Save;
using Application.Resources.OrderedCourses.Get;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AsyncsController : BaseController
    {
        private readonly IHttpClientFactory httpClientFactory;
        
        public AsyncsController(IMapper mapper, IHttpClientFactory httpClientFactory) : base(mapper)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FetchExternalDataAsync()
        {
            var httpClient = httpClientFactory.CreateClient("ExternalFlaskAPI");
            var entitiesAmount = new[] {100, 200, 500, 1000, 2000, 5000};
            var resultInstances = new List<AsyncOperationsInstance>();

            var stopWatch = new Stopwatch();

            for (var i = 0; i < 30; i++)
            {
                foreach (var amount in entitiesAmount)
                {
                    var resultInstance = new AsyncOperationsInstance
                    {
                        EntitiesAmount = amount
                    };
                    
                    stopWatch.Start();
                    var httpRequestMessage = await httpClient.GetAsync($"random/entities/{amount}");

                    if (httpRequestMessage.IsSuccessStatusCode)
                    {
                        await httpRequestMessage.Content.ReadAsStreamAsync();
                        stopWatch.Stop();
                        resultInstance.IsError = false;
                        resultInstance.ExecutionTime = stopWatch.ElapsedMilliseconds;
                        resultInstances.Add(resultInstance);
                        continue;
                    }
                    
                    stopWatch.Stop();
                    resultInstance.IsError = true;
                    resultInstance.ExecutionTime = stopWatch.ElapsedMilliseconds;
                    resultInstances.Add(resultInstance);
                }
                stopWatch.Reset();
            }

            var result = new AsyncOperationsResult
            {
                AverageTimeFor100Entities =
                    resultInstances.Where(p => p.EntitiesAmount == 100).Average(p => p.ExecutionTime),
                AverageTimeFor200Entities =
                    resultInstances.Where(p => p.EntitiesAmount == 200).Average(p => p.ExecutionTime),
                AverageTimeFor500Entities =
                    resultInstances.Where(p => p.EntitiesAmount == 500).Average(p => p.ExecutionTime),
                AverageTimeFor1000Entities =
                    resultInstances.Where(p => p.EntitiesAmount == 1000).Average(p => p.ExecutionTime),
                AverageTimeFor2000Entities =
                    resultInstances.Where(p => p.EntitiesAmount == 2000).Average(p => p.ExecutionTime),
                AverageTimeFor5000Entities =
                    resultInstances.Where(p => p.EntitiesAmount == 5000).Average(p => p.ExecutionTime),

                PercentageErrorFor100Entities =
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 100 && p.IsError)) /
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 100)),
                PercentageErrorFor200Entities =
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 200 && p.IsError)) /
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 200)),
                PercentageErrorFor500Entities =
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 500 && p.IsError)) /
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 500)),
                PercentageErrorFor1000Entities =
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 1000 && p.IsError)) /
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 1000)),
                PercentageErrorFor2000Entities =
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 2000 && p.IsError)) /
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 2000)),
                PercentageErrorFor5000Entities =
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 5000 && p.IsError)) /
                    Convert.ToDouble(resultInstances.Count(p => p.EntitiesAmount == 5000)),
            };

            return Ok(result);
        }
    }
}