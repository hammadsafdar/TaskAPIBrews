using BrewTask.Models.Dtos;
using BrewTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrewTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewsController : ControllerBase
    {
        private readonly iBrewsService _brewService;
        public BrewsController(iBrewsService brewService) 
        {
            _brewService = brewService;
        }

        [HttpPost]
        [Route("CreateBrew")]
        public async Task<IActionResult> CreateBrew(BrewsCreateRequestDto BrewsRequestDto)
        {
            return Ok(await _brewService.CreateBrew(BrewsRequestDto));
        }

        [HttpPost]
        [Route("GetAllBrew")]
        public async Task<IActionResult> GetAllBrew()
        {
            return Ok(await _brewService.GetAllBrew());
        }

        [HttpPost]
        [Route("GetByNameBrew")]
        public async Task<IActionResult> GetByNameBrew(string queryTerm)
        {
            return Ok(await _brewService.GetByNameBrew(queryTerm));
        }

        [HttpPost]
        [Route("UpdateBrewRating")]
        public async Task<IActionResult> UpdateBrewRating(UpdateBrewRatingDto requestDto)
        {
            return Ok(await _brewService.UpdateBrewRating(requestDto));
        }
    }
}
