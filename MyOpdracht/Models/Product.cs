using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Models
{
    public class Product
    {
      
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        


        public int ItemId { get; set; }


        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
        public Item Item { get; set; }
    }
}
