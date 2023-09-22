using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyOpdracht.Data;
using MyOpdracht.Models;

namespace MyOpdracht.Pages.Admin
{
    public class EditModel : PageModel
    {
        private MyOpdrachtContext _context;

        public EditModel(MyOpdrachtContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel AddEditProduct { get; set; }
        [BindProperty]
        public List<int> selectedGroups { get; set; }

        public List<int> GroupsProduct { get; set; }
        public void OnGet(int id)
        {

            var product = _context.Products.Include(p => p.Item)
                .Where(p => p.Id == id)
                .Select(s => new AddEditProductViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    QuantityInstock = s.Item.QuantityInStock,
                    Price = s.Item.Price
                }).FirstOrDefault();
            AddEditProduct = product;


            product.Categories = _context.Categories.ToList();
            GroupsProduct = _context.CategoryToProducts.Where(c => c.ProductId == id)
                .Select(s => s.CategoryId).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var product = _context.Products.Find(AddEditProduct.Id);
            var item = _context.Items.First(p => p.Id == product.ItemId);

            product.Name = AddEditProduct.Name;
            product.Description = AddEditProduct.Description;
            item.Price = AddEditProduct.Price;
            item.QuantityInStock = AddEditProduct.QuantityInstock;

            
            _context.SaveChanges();


            if (AddEditProduct.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    product.Id + Path.GetExtension(AddEditProduct.Picture.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    AddEditProduct.Picture.CopyTo(stream);
                }
            }

            // Edit Group
            _context.CategoryToProducts.Where(c => c.ProductId == product.Id).ToList()
                .ForEach(g => _context.CategoryToProducts.Remove(g));

            if (selectedGroups.Any() && selectedGroups.Count > 0)
            {
                foreach (int gr in selectedGroups)
                {
                    _context.CategoryToProducts.Add(new CategoryToProduct()
                    {
                        CategoryId = gr,
                        ProductId = product.Id
                    });
                }

                _context.SaveChanges();
            }

            return RedirectToPage("Index");



        }

    }
}
