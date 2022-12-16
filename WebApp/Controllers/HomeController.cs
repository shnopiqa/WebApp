using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;
        public HomeController(ApplicationContext db,ILogger<HomeController> logger)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType),
                Categories = _db.Category
            };
            return View(homeVM);
        }
        public IActionResult Details(int id) 
        {
            DetailsVM DetailsVM = new DetailsVM()
            {
                Product = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType)
                .Where(u => u.Id == id).FirstOrDefault(),
                ExcistInCart = false
            };
            return View(DetailsVM);
        }
        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShopingCart> ShoppingCartList = new List<ShopingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart)!=null
                && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0) 
            {
                ShoppingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            ShoppingCartList.Add(new ShopingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, ShoppingCartList);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}