using MyOpdracht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Data.Repositories
{
    
    public class GroupRepository : IGroupRepository
    {
        private MyOpdrachtContext _context;
        public GroupRepository(MyOpdrachtContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            return _context.Categories
                .Select(c => new ShowGroupViewModel() 
                {
                    GroupId=c.Id,
                    Name=c.Name,
                    ProductCount=_context.CategoryToProducts.Count(g=> g.CategoryId==c.Id)
                }).ToList();
        }
    }
}
