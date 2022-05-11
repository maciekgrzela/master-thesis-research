using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Ingredients.Get;
using Application.Resources.Ingredients.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class IngredientsController : BaseController
    {
        private readonly IIngredientService ingredientService;
        public IngredientsController(IMapper mapper, IIngredientService ingredientService) : base(mapper)
        {
            this.ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await ingredientService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientResource>>(result.Type);

            return GenerateResponse<IEnumerable<IngredientResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoursesCategoryAsync(Guid id)
        {
            var result = await ingredientService.GetIngredientAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Ingredient, IngredientResource>(result.Type);

            return GenerateResponse<IngredientResource>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveIngredientResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var ingredient = mapper.Map<SaveIngredientResource, Ingredient>(resource);
            var result = await ingredientService.SaveAsync(ingredient);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Ingredient>(HttpStatusCode.NoContent, new Ingredient());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveIngredientResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var ingredient = mapper.Map<SaveIngredientResource, Ingredient>(resource);
            var result = await ingredientService.UpdateAsync(id, ingredient);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Ingredient>(HttpStatusCode.NoContent, new Ingredient());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await ingredientService.DeleteAsync(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Ingredient>(HttpStatusCode.NoContent, new Ingredient());
        }
    }
}