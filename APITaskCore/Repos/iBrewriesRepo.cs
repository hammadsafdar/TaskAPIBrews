using APITaskCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITaskCore.Repos
{
    public interface iBrewriesRepo
    {
        bool SaveChanges();
        IEnumerable<Breweries> GetAllBreweries();
        Breweries GetBrewByID(int Id);
        void CreateBrewries(Breweries brew);
        bool BrewExists(int BrewID);
        bool DeleteBrew(int BrewID);
        List<Breweries> SearchBrew(string SearchVal);
        void UpdateBrew(int BrewID, Breweries Brew);
        bool BrewExistsByName(string BrewName);
        
        void CreateRating(int BrewID, double RateVal);
       
    }
}
