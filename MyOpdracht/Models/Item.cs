using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Models
{
    public class Item
    {
        
        public int Id { get; set; }
        
        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public DateTime Date { get; set; }



        public Product Product { get; set; }
    }
}
