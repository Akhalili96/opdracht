using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyOpdracht.Data;
using MyOpdracht.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyOpdrachtContext _context;
        private static Cart _cart = new Cart();
        public HomeController(ILogger<HomeController> logger,MyOpdrachtContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public IActionResult Index()
        {

            
            var product = _context.Products.ToList();
            return View(product);

           
        }

        [HttpGet]
        public async Task<IActionResult> Index(string Productsearch)
        {
            ViewData["GetProductsearch"] = Productsearch;
            var product = from p in _context.Products select p;
            if (!string.IsNullOrEmpty(Productsearch))
            {
                product = product.Where(p => p.Name.Contains(Productsearch) || p.Description.Contains(Productsearch));
            }
            return View(await product.AsNoTracking().ToListAsync());
        }





        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(p => p.Item)
                .SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.Id == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
                .ToList();

            var vm = new DetailViewModel()
            {
                Product=product,
                Categories=categories
            };

            return View(vm);
        }

        
        public IActionResult AddToCart(int itemId)
        {
            var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.ItemId == itemId);
            if (product != null)
            {
                var cartItem = new CartItem()
                {
                    Item = product.Item,
                    Date = DateTime.Now,
                    Quantity=1
                };

                _cart.addItem(cartItem);
            }
            return RedirectToAction("ShowCart");
        }

        
        public IActionResult ShowCart()
        {
            var Cartvm = new CartViewModel() 
            {
                CartItems=_cart.CartItems,
                OrderTotal=_cart.CartItems.Sum(c=>c.getTotalPrice())
            };

            return View(Cartvm);
           
        }

        public IActionResult RemoveCart(int itemId)
        {
            _cart.removeItem(itemId);
            return RedirectToAction("ShowCart");
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
