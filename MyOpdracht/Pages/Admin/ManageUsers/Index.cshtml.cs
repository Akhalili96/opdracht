using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyOpdracht.Data;
using MyOpdracht.Models;

namespace MyOpdracht.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly MyOpdracht.Data.MyOpdrachtContext _context;

        public IndexModel(MyOpdracht.Data.MyOpdrachtContext context)
        {
            _context = context;
        }

        public IList<Users> Users { get;set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
