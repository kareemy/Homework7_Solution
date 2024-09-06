using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Homework7_Solution.Models;

namespace Homework7_Solution.Pages_Orders
{
    public class IndexModel : PageModel
    {
        private readonly Homework7_Solution.Models.AppDbContext _context;

        public IndexModel(Homework7_Solution.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        // Paging support
        // PageNum is the current page number we are on
        // PageSize is how many records will be displayed per page. 
        // PageNum needs BindProperty because the user decides which page we are on.
        // The user selects the page number
        // SupportsGet = true allows us to pass the PageNum through the URL with an HTTP Get Parameter 
        // This is necessary, because page numbers are not passed through normal forms
        [BindProperty(SupportsGet = true)]
        public int PageNum {get; set;} = 1;
        public int PageSize {get; set;} = 10;
        public int TotalPages {get; set;}

        // Sorting support
        [BindProperty(SupportsGet = true)]
        public string CurrentSort {get; set;} = string.Empty;
        // Search support
        [BindProperty(SupportsGet = true)]
        public string CurrentSearch {get; set;} = string.Empty;

        public async Task OnGetAsync()
        {
            var query = _context.Orders.Include(o => o.Products!).ThenInclude(p => p.Product).Select(s => s);

            if (!string.IsNullOrEmpty(CurrentSearch))
            {
                query = query.Where(o => o.CustomerName.ToUpper().Contains(CurrentSearch.ToUpper())
                    || o.Products!.Any(p => p.Product.Name.ToUpper().Contains(CurrentSearch.ToUpper())));
            }

            switch (CurrentSort)
            {
                case "name_asc":
                    query = query.OrderBy(o => o.CustomerName);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(o => o.CustomerName);
                    break;
                case "date_asc":
                    query = query.OrderBy(o => o.OrderDate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(o => o.OrderDate);
                    break;
            }

            TotalPages = (int)Math.Ceiling(_context.Orders.Count() / (double)PageSize);

            Order = await query.Skip((PageNum-1)*PageSize).Take(PageSize).ToListAsync();
        }
    }
}
