using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreewayIT.Test.DAL
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee Get(int id);
        void Add(Employee employee);

        void Update(Employee employee);

        void DeleteById(int id);

    }
}
