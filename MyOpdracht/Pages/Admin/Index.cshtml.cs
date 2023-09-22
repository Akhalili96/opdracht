using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyOpdracht.Data;
using MyOpdracht.Models;

namespace MyOpdracht.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private MyOpdrachtContext _context;

        public IndexModel(MyOpdrachtContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products { get; set; }
        public void OnGet()
        {
            Products = _context.Products.Include(p => p.Item);
        }



        public void OnPost()
        {
        }
    }
}
