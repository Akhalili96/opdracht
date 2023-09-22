using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOpdracht.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Controllers
{
    public class ProductController : Controller
    {
        private MyOpdrachtContext _context;

        public ProductController(MyOpdrachtContext context)
        {
            _context = context;
        }

        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id, string name)
        {
            ViewData["GroupName"] = name;
            var product = _context.CategoryToProducts
                .Where(c => c.CategoryId == id)
                .Include(c => c.Product)
                .Select(c => c.Product)
                .ToList();

            return View(product);
        }
    }
}
