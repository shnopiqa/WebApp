using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationContext _db; 
        public CartController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            List<ShopingCart> shopingCartsList = new List<ShopingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart)!= null
                && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0) 
            {
                //session excist 
                shopingCartsList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shopingCartsList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));
            return View(prodList);
        }
    }
}
