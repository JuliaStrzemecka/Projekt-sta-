using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreewayIT.Test.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private TestFreewayITEntities db = new TestFreewayITEntities();
        public List<Employee> GetAll()
        {
            return db.Employees.Include("City").ToList();
        }

        public Employee Get(int id)
        {
          return db.Employees.Find(id);
        }

        public void Add(Employee employee) { 

            db.Employees.Add(employee);
            db.SaveChanges(); 
        }

        public void Update(Employee employee)
        {
            var employeeEntity = db.Employees.First(a => a.ID == employee.ID);
            employeeEntity.FirstName = employee.FirstName;
            employeeEntity.LastName = employee.LastName;
            employeeEntity.BirthMonth = employee.BirthMonth; 
            employeeEntity.City = employee.City;
            db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var employee = db.Employees.Find(id);
            if(employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }
        }
    }
}
