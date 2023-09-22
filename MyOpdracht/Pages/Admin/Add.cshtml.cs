using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyOpdracht.Data;
using MyOpdracht.Models;

namespace MyOpdracht.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MyOpdrachtContext _contex;
        public AddModel(MyOpdrachtContext context)
        {
            _contex = context;
        }

        [BindProperty]
        public AddEditProductViewModel AddEditProduct { get; set; }

        [BindProperty]
        public List<int> selectedGroups { get; set; }
        public void OnGet()
        {
            AddEditProduct = new AddEditProductViewModel()
            {
                Categories= _contex.Categories.ToList()
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var item = new Item()
            {
                Price = AddEditProduct.Price,
                QuantityInStock = AddEditProduct.QuantityInstock,
                Date = DateTime.Now

            };

            _contex.Add(item);
            _contex.SaveChanges();

            var product = new Product()
            {
                Name = AddEditProduct.Name,
                Item = item,
                Description = AddEditProduct.Description
            };

            _contex.Add(product);
            _contex.SaveChanges();
            product.ItemId = product.Id;
            _contex.SaveChanges();

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

            // Select Group

            if(selectedGroups.Any() && selectedGroups.Count > 0)
            {
                foreach(int gr in selectedGroups)
                {
                    _contex.CategoryToProducts.Add(new CategoryToProduct()
                    {
                        CategoryId= gr,
                        ProductId=product.Id
                    });
                }
            }

            return RedirectToPage("Index");

        }
    }
}
