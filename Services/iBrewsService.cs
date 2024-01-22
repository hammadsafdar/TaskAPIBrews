using BrewTask.Models.Dtos;
using BrewTask.Models.GenericResponse;

namespace BrewTask.Services
{
    public interface iBrewsService
    {
        Task<ResponseModel> CreateBrew(BrewsCreateRequestDto requestDto);
        Task<ResponseModel> GetAllBrew();
        Task<ResponseModel> GetByNameBrew(string Queryterm);
        Task<ResponseModel> UpdateBrewRating(UpdateBrewRatingDto requestDto);
    }
}
