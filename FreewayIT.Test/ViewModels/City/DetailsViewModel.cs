using FreewayIT.Test.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreewayIT.Test.ViewModels.City
{
    public class DetailsViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }

        internal DAL.City ToModel()
        {
            return new DAL.City()
            {
                ID = ID,
                Name = Name
            };
        }

        internal void Initialize(DAL.City city)
        {
            ID = city.ID;
            Name = city.Name;
            Employees = city.Employees?.ToList() ?? new List<Employee>();
        }
    }
}