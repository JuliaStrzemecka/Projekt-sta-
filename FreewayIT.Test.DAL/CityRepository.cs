using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreewayIT.Test.DAL
{
    public class CityRepository : ICityRepository
    {
        private TestFreewayITEntities db = new TestFreewayITEntities();


        public City Get(int id)
        {
            return db.Cities.Include("Employees").FirstOrDefault(c => c.ID == id);
        }

        public List<City> GetAll()
        {
             return db.Cities.ToList();
        }
        public void Add(City city)
        {
            db.Cities.Add(city);
            db.SaveChanges();
        }

        public void Update(City city)
        {
             var cityEntity = db.Cities.First(a => a.ID == city.ID); ;
            cityEntity.Name = city.Name;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            City city = db.Cities.Find(id);
            if(city != null)
            {
                db.Cities.Remove(city);
                db.SaveChanges();
            }
        }
    }
}
