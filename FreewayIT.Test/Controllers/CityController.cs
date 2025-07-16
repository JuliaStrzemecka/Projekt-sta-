using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreewayIT.Test.DAL;
using FreewayIT.Test.ViewModels.City;

namespace FreewayIT.Test.Controllers
{
     
    public class CityController : Controller
    {

  
       ICityRepository repo = new CityRepository(); 

        //
        // GET: /City/

        public ActionResult Index()
        { 
            return View(repo.GetAll().ToList());
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateEditCityViewModel city)
        {
            var existingCity = repo.GetAll().FirstOrDefault(c => c.Name.ToLower().Trim() == city.Name.ToLower().Trim());

            if (existingCity != null)
            {
                ModelState.AddModelError("Name", "Already exists");
            }

            if (ModelState.IsValid)
            {
                repo.Add(city.ToModel());
                return RedirectToAction("Index");
            }

            return View(city);
        }

        public ActionResult Edit(int id)
        {
            CreateEditCityViewModel viewModel = new CreateEditCityViewModel();

            City city = repo.Get(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            viewModel.Initialize(city);
            return View(viewModel);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEditCityViewModel viewModel)
        {
            var existingCity = repo.GetAll()
                .FirstOrDefault(c => c.Name.ToLower().Trim() == viewModel.Name.ToLower().Trim() && c.ID != viewModel.ID);

            if (existingCity != null)
            {
                ModelState.AddModelError("Name", "Miasto o tej nazwie już istnieje.");
            }

            if (ModelState.IsValid)
            {
                repo.Update(viewModel.ToModel());
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            DetailsViewModel viewModel = new DetailsViewModel();
            City city = repo.Get(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            viewModel.Initialize(city);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            City city = repo.Get(id);
            if (city== null)
            {
                return HttpNotFound();
            }

            repo.Delete(id);

            return RedirectToAction("Index");
        }
        
    }
}

