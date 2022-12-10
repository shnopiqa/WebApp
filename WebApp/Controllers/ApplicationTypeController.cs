using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationContext _db;
        public ApplicationTypeController(ApplicationContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _db.ApplicationType;
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
           _db.ApplicationType.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}
