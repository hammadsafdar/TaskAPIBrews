using BrewTask.DBCore.AppContext;
using BrewTask.DBCore.Entities;
using BrewTask.Models.Dtos;
using BrewTask.Models.GenericResponse;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BrewTask.Services
{
    public class BrewsService : iBrewsService
    {
        private readonly AppDbContext _context;
        private readonly ResponseModel _model;
        public BrewsService(AppDbContext context, ResponseModel model) 
        {
            _context = context;
            _model = model;
        }
        public async Task<ResponseModel> CreateBrew(BrewsCreateRequestDto requestDto)
        {
            try
            {
                _model.SuccessModel();

                if(requestDto.Rating != null)
                {
                    if (requestDto.Rating < 0 || requestDto.Rating > 5)
                    {
                        _model.ValidationModel();
                        _model.ResponseMessage = "rating can not be less than 1 and more than 5";
                        _model.Success = false;
                        return _model;
                    }
                }
               
                var brew = _context.Beers.FirstOrDefault(b => b.Name == requestDto.Name);
                if (brew == null)
                {
                    BrewsEntity obj = new BrewsEntity();
                    obj.Name = requestDto.Name;
                    obj.Type = requestDto.Type;
                    obj.AverageRating = requestDto.Rating ?? 0;

                    var data = _context.Beers.Add(obj);
                    await _context.SaveChangesAsync();

                    BrewRatingsEntity obj1= new BrewRatingsEntity();
                    obj1.RatingCount = requestDto.Rating ?? 0;
                    obj1.BrewId = data.Entity.Id;

                    var data1 = _context.Ratings.Add(obj1);
                    await _context.SaveChangesAsync();

                    _model.ResponseMessage = "Brew saved successfully";
                    _model.Data = data.Entity;
                    _model.Success = true;
                    return _model;
                }
                else
                {
                    _model.ValidationModel();
                    _model.Success = false;
                    _model.ResponseMessage = "Brew already exists";
                    return _model;
                }

              
            }
            catch (Exception ex)
            {
                _model.ResponseMessage = ex.Message;
                _model.ExceptionModel();
                _model.Success = false;
                return _model;
            }
        }

        public async Task<ResponseModel> GetAllBrew()
        {
            try
            {
                _model.SuccessModel();

                var data = await (from b in _context.Beers
                                  join r in _context.Ratings on b.Id equals r.BrewId
                                  select new
                                  {
                                      Name = b.Name,
                                      Type = b.Type,
                                      AverageRating = Math.Round(Convert.ToDecimal(b.AverageRating), 2),
                                      TotalRatingCount = Math.Round(Convert.ToDecimal(r.RatingCount), 2),
                                  }).ToListAsync();
                if (data != null)
                {
                    _model.ResponseMessage = "Data fetched successfully";
                    _model.Data = data;
                    _model.Success = true;
                    return _model;
                }
                else
                {
                    _model.ResponseMessage = "no data found";
                    _model.Data = null;
                    _model.Success = true;
                    return _model;
                }

            }
            catch (Exception ex)
            {
                _model.ExceptionModel();
                _model.ResponseMessage= ex.Message;
                _model.Success = false;
                return _model;
            }
        }

        public async Task<ResponseModel> GetByNameBrew(string Queryterm)
        {
            try
            {
                _model.SuccessModel();

                var data = await (from b in _context.Beers
                                  join r in _context.Ratings on b.Id equals r.BrewId
                                  where b.Name.Contains(Queryterm)
                                  select new
                                  {
                                      Name = b.Name,
                                      Type = b.Type,
                                      AverageRating = Math.Round(Convert.ToDecimal(b.AverageRating), 2),
                                      TotalRatingCount = Math.Round(Convert.ToDecimal(r.RatingCount), 2),
                                  }).ToListAsync();

                if (data != null)
                {
                    _model.Data = data;
                    _model.Success = true;
                    _model.ResponseMessage = "matching brews found";
                }
                return _model;
            }
            catch (Exception ex)
            {
                _model.ExceptionModel();
                _model.ResponseMessage= ex.Message;
                _model.Success = false;
                return _model;
            }
           
        }

        public async Task<ResponseModel> UpdateBrewRating(UpdateBrewRatingDto requestDto)
        {
            try
            {
                _model.SuccessModel();

                if (requestDto.Rating < 0 || requestDto.Rating > 5)
                {
                    _model.ValidationModel();
                    _model.ResponseMessage = "rating can not be less than 1 and more than 5";
                    _model.Success = false;
                    return _model;
                }

                var beer = await _context.Beers.FirstOrDefaultAsync(b => b.Id == requestDto.BrewId);

                if (beer == null)
                {
                    _model.ValidationModel();
                    _model.ResponseMessage = "no matching beer found";
                    _model.Success = false;
                    return _model;
                }

                BrewRatingsEntity obj = new BrewRatingsEntity();
                obj.BrewId = requestDto.BrewId;
                obj.RatingCount = requestDto.Rating;
                _context.Ratings.Update(obj);
                _context.SaveChanges();

                var totalrating = _context.Ratings.Where(x => x.BrewId == requestDto.BrewId).ToList();
                if (totalrating != null)
                {
                    var averagerating = totalrating.Sum(x=>x.RatingCount)/totalrating.Count;
                    beer.AverageRating= averagerating;
                    _context.Beers.Update(beer);
                    _context.SaveChanges();
                }

                var updatedbeer = await (from b in _context.Beers
                                         join r in _context.Ratings on b.Id equals r.BrewId
                                         where r.Id == requestDto.BrewId
                                         select new
                                         {
                                             Name = b.Name,
                                             Type = b.Type,
                                             AverageRating = Math.Round(Convert.ToDecimal(b.AverageRating), 2),
                                             TotalRatingCount = Math.Round(Convert.ToDecimal(r.RatingCount), 2),
                                         }).FirstOrDefaultAsync();

                _model.Data = updatedbeer;
                _model.ResponseMessage = "average updated successfully";
                _model.Success = true;
                return _model;
            }
            catch (Exception ex)
            {
                _model.ExceptionModel();
                _model.Success = false;
                _model.ResponseMessage = ex.Message;
                return _model;
            }
        }
    }
}
