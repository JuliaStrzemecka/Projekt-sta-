using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreewayIT.Test.ViewModels.City
{
    public class CreateEditCityViewModel
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

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
        }
    }
}