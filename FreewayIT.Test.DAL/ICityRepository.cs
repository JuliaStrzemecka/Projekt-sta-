using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreewayIT.Test.DAL;

namespace FreewayIT.Test.DAL
{
    public interface ICityRepository
    {
        List<City> GetAll();
        City Get(int id);
        void Add(City city);

        void Update(City city);

        void Delete(int id);
    }
}
