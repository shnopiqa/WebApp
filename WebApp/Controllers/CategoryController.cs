using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationContext _db;
        public CategoryController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objtList = _db.Category;
            return View(objtList);
        }
        //GET CREATE 
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            _db.Category.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
