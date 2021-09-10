using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITaskCore.DataModels;
using APITaskCore.Models;
using Microsoft.EntityFrameworkCore;

namespace APITaskCore.Repos
{
    public class BrewriesRepo : iBrewriesRepo
    {
        private readonly ApplicationContext _db;
        
        bool isactivetrue = true;
        //bool isactivefalse = false;

        public BrewriesRepo(ApplicationContext context)
        {
            _db = context;
            
        }

        public bool BrewExists(int BrewID)
        {
            return _db.Breweries.Any(p => p.BrewId == BrewID && p.IsActive == isactivetrue);
        }

        public void CreateBrewries(Breweries brew)
        {
            DateTime DateTime = DateTime.Now;
            string LoggedInnUser = "Logged Inn User";

            brew.AverageRating = 0.00;
            brew.IsActive = true;
            brew.CreatedBy = LoggedInnUser;
            brew.CreatedDate = DateTime;
            brew.ModifiedBy = LoggedInnUser;
            brew.ModifiedDate = DateTime;

            _db.Breweries.Add(brew);
        }

        public void CreateRating(int BrewID, double RateVal)
        {
            Ratings _rating = new Ratings();
            var date = DateTime.Now;
            var user = "Logged Inn User";

            _rating.BrewId = BrewID;
            _rating.AllRatings = RateVal;
            _rating.IsActive = true;
            _rating.CreatedBy = user;
            _rating.CreatedDate = date;
            _rating.ModifiedBy = user;
            _rating.ModifiedDate = date;

            _db.Ratings.Add(_rating);
            SaveChanges();

            var totalSum = _db.Ratings.Where(r => r.BrewId == BrewID).Sum(s=>s.RatingId);
            var count = _db.Ratings.Where(c => c.BrewId == BrewID).Count();

            double AveTotal = totalSum / count;

            var bid = GetBrewByID(BrewID);

            bid.ModifiedBy = user;
            bid.ModifiedDate = date;
            bid.AverageRating = AveTotal;
            _db.Entry(bid).State = EntityState.Modified;
            SaveChanges();
        }

        public bool DeleteBrew(int BrewID)
        {
            var isBrewExist = BrewExists(BrewID);
            if (isBrewExist == true)
            {
                Breweries breweries = GetBrewByID(BrewID);
                _db.Breweries.Remove(breweries);
                SaveChanges();

                List<Ratings> ratings = _db.Ratings.Where(r => r.BrewId==BrewID).ToList();
                foreach (var rate in ratings)
                {
                    _db.Ratings.Remove(rate);
                    SaveChanges();
                }

                return true;
            }
            return false;
        }

        public IEnumerable<Breweries> GetAllBreweries()
        {
            return _db.Breweries.Where(x => x.IsActive == isactivetrue).ToList();
        }

        public Breweries GetBrewByID(int Id)
        {
            var breweryes = _db.Breweries.Find(Id);
            return breweryes;
        }

        public List<Breweries> SearchBrew(string searchVal)
        {
            var brews = GetAllBreweries();
            brews = brews.Where(s => s.BrewName.ToLower().Contains(searchVal));
            var result = brews.ToList();
            return result;
        }

        public bool SaveChanges()
        {
            return (_db.SaveChanges() >= 0);
        }

        public void UpdateBrew(int BrewID, Breweries Brew)
        {
            _db.Entry(Brew).State = EntityState.Modified;
        }

        public bool BrewExistsByName(string BrewName)
        {
            return _db.Breweries.Any(p => p.BrewName == BrewName);
        }

    }
}
