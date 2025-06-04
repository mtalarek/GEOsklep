using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using GEOsklep.Repositories;
using GEOsklep.Data;
using GEOsklep.Models;

namespace GEOsklep.Controllers
{
    public class ArtykulController : Controller
    {
        private SklepManager _manager;

        public ArtykulController(SklepManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var artykuls = _manager.GetArtykuls();
            return View(artykuls);
        }

        //public IActionResult Welcome(string name, int ID =1)
        //{
        //    ViewData["dane1"] = "dane1 " + name;
        //    ViewData["ID"] = ID;
        //    return View();
        //}

        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Add(Artykul artykul, ArtykulBasket artykulbasket)
        //{
        //    _manager.AddArtykul(artykul, artykulbasket);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult Add(ArtykulBasket artykulbasket)
        {
            _manager.AddArtykul(artykulbasket);
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            var artykulToDelete = _manager.GetArtykul(id);
            return View(artykulToDelete);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _manager.RemoveArtykul(id);
            return RedirectToAction("Index");
        }

        //[HttpGet]

        //public IActionResult Buy(int id)
        //{
        //    var artykulToBuy = _manager.GetArtykul(id);
        //    return View(artykulToBuy);
        //}

        //[HttpPost]
        //public IActionResult Buy(int id)
        //{
        //    _manager.RemoveArtykul(id);
        //    return RedirectToAction("Index");
        //}

        [HttpGet]

        public IActionResult Edit(int id)
        {
            var artykulToEdit = _manager.GetArtykul(id);
            return View(artykulToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Artykul artykul)
        {
            _manager.UpdateArtykul(artykul);
            return RedirectToAction("Index");
        }


        
        public IActionResult Koszyk()
        {
            var artykuls = _manager.GetArtykulsBasket();
            return View(artykuls);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
