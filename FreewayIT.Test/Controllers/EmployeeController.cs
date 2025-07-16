using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreewayIT.Test.DAL;
using FreewayIT.Test.ViewModels.Employees;

namespace FreewayIT.Test.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository repo = new EmployeeRepository();

        ICityRepository repoCities = new CityRepository();

        //
        // GET: /Employee/
        public ActionResult Index()
        {
            var employees = repo.GetAll();
            return View(employees.ToList());
        }

       
        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            CreateEditEmployeeViewModel viewModel = new CreateEditEmployeeViewModel();
            var citiesDb = repoCities.GetAll();
            var cities = citiesDb.Select(a=> new SelectListItem() {Text = a.Name, Value=a.ID.ToString() }).ToList();
            viewModel.Initialize(new Employee(), cities);
            ViewBag.CityID = new SelectList(cities, "ID", "Name");
            return View(viewModel);
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditEmployeeViewModel employee)
        {
            var existingCity = repoCities.Get(Convert.ToInt32(employee.CityID));

            if (existingCity == null)
            {
                ModelState.AddModelError("CityID", $"City with ID {employee.CityID} does not exist.");  

                employee.Cities = repoCities
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.Name
                    }).ToList();

                return View(employee);
            }

            if (ModelState.IsValid)
            {
                repo.Add(employee.ToModel());
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(repoCities.GetAll(), "ID", "Name"); 
            return View(employee);
        }

        //
        // GET: /Employee/Edit/5


        public ActionResult Edit(int id = 0)
        {
            CreateEditEmployeeViewModel viewModel = new CreateEditEmployeeViewModel();
            Employee employee = repo.Get(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            var citiesDb = repoCities.GetAll();
            var cities = citiesDb.Select(a => new SelectListItem() { Text = a.Name, Value = a.ID.ToString() }).ToList();
            ViewBag.CityID = new SelectList(cities);
            viewModel.Initialize(employee, cities);
            return View(viewModel);
        }


        // 
        // POST: /Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEditEmployeeViewModel employee)
        {
            if (!ModelState.IsValid) throw new Exception("ViewModel is Invalid");

            var existingCity = repoCities.Get(Convert.ToInt32(employee.CityID));

            if(existingCity == null)
            {
                    employee.Cities = repoCities
                        .GetAll()
                        .Select(c => new SelectListItem
                        {
                            Value = c.ID.ToString(),
                            Text = c.Name
                        }).ToList();

                    return View(employee);
            }

            Employee employee1 = repo.Get(employee.ID);


            if (employee == null)
            {
                return HttpNotFound();
            }
            
            employee1.CityID = employee.CityID;
            
            repo.Update(employee1);
            return RedirectToAction("Index");
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Employee employee = repo.Get(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            repo.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}