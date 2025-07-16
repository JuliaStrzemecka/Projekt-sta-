using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreewayIT.Test.ViewModels.Employees
{
    public class CreateEditEmployeeViewModel
    {
        public int ID { get; set; }
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }


        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        public string BirthMonth { get; set; }

        public Nullable<int> CityID { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set;}

        internal DAL.Employee ToModel()
        {
            return new DAL.Employee()
            {
                ID = ID,
                FirstName = FirstName,
                LastName = LastName,
                BirthMonth = BirthMonth,
                CityID = CityID
            };
        }

        internal void Initialize(DAL.Employee employee, IEnumerable<SelectListItem> cities)
        {
            ID = employee.ID;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            BirthMonth = employee.BirthMonth;
            CityID = employee.CityID;
            Cities = cities;
        }
    }
}