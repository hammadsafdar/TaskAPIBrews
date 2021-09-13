using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITaskCore.Models;
using APITaskCore.Repos;

namespace APITaskCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class BreweriesController : ControllerBase
    {
        private readonly iBrewriesRepo _repo;

        public BreweriesController(iBrewriesRepo repo)
        {
            _repo = repo;
        }

        // GET: Breweries/GetBreweryes
        [HttpGet]
        public ActionResult<IEnumerable<Breweries>> GetBreweryes()
        {
            var Brewries = _repo.GetAllBreweries();
            return Ok(Brewries);
        }

        // GET: Breweries/GetBreweryes/5
        [HttpGet("{id}")]
        public ActionResult<Breweries> GetBreweryes(int id)
        {
            if (id < 1 )
            {
                return NotFound();
            }
            var brew = _repo.GetBrewByID(id);

            if (brew == null)
            {
                return NotFound();
            }

            return Ok(brew);
        }

        // PUT: Breweries/UpdateBreweryes/5

        [HttpPut("{id}")]
        public ActionResult PutBreweryes(int id, Breweries breweryes)
        {
            if (id != breweryes.BrewId)
            {
                return BadRequest();
            }
             _repo.UpdateBrew(id, breweryes);
            
            try
            {
                var updated = _repo.SaveChanges();
                return Ok(updated);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repo.BrewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: Breweries/CreateBreweryes
        [HttpPost]
        public ActionResult PostBreweryes(Breweries brew)
        {
            if (ModelState.IsValid)
            {
                if (brew.BrewName == "" || brew.BrewName == null)
                {
                    return BadRequest();
                }
                var b = _repo.BrewExistsByName(brew.BrewName);
                if (b == true)
                {
                    return BadRequest();
                }

                _repo.CreateBrewries(brew);
                _repo.SaveChanges();

                return CreatedAtAction("GetBreweryes", new { Brewid = brew.BrewId }, brew);
            }
            else
            {
                return BadRequest();
            }
          
        }

        // DELETE: Breweries/DeleteBreweryes/5
        [HttpDelete("{id}")]
        public ActionResult<Breweries> DeleteBreweryes(int id)
        {
            var breweryes = _repo.BrewExists(id);
            if (breweryes == false)
            {
                return NotFound();
            }

            _repo.DeleteBrew(id);
            return Ok(breweryes);
        }

        //Breweries/AddRating
        [HttpPost]
        [Route("AddRating")]
        public ActionResult AddBreweryRating(int Brewid, double RateVal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bid = _repo.BrewExists(Brewid);
                    if (bid == true)
                    {
                        if (RateVal > 5 || RateVal < 0)
                        {
                            return BadRequest();
                        }
                        else
                        {
                            _repo.CreateRating(Brewid, RateVal);
                            return CreatedAtAction("GetBreweryes", Brewid);
                        }
                       
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
              
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        //Breweries/Search
        [HttpGet]
        [Route("Search")]
        public ActionResult<IEnumerable<Breweries>> Serach(string searchVal)
        {
            if (!String.IsNullOrEmpty(searchVal))
            {
                var lowerVal = searchVal.ToLower();
                var result = _repo.SearchBrew(lowerVal);
                
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            else
            {
                return BadRequest();
            }
            
        }

    }
}
