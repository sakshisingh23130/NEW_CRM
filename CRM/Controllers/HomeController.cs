using CRM.Data;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // 🌐 Public Welcome Page
        [AllowAnonymous]
        public IActionResult Welcome()
        {
            return View();
        }

        // 📊 Main Dashboard (Requires Login)
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        // ⚙️ Settings Page (Example)
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        // 🔎 UNIVERSAL SEARCH (Customers + Employees)
        [Authorize]  // Optional: remove if you want everyone to search
        public IActionResult SearchAll(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return View(new List<SearchResult>());

            query = query.ToLower();

            var customers = _context.Customers
                .Where(c => c.name.ToLower().Contains(query))
                .Select(c => new SearchResult
                {
                    Type = "Customer",
                    Name = c.name,
                    Email = c.email,
                    Phone = c.phoneNumber,
                    Extra = ""
                });

            var employees = _context.Employees
                .Where(e => e.name.ToLower().Contains(query))
                .Select(e => new SearchResult
                {
                    Type = "Employee",
                    Name = e.name,
                    Email = e.email,
                    Phone = e.phoneNumber,
                    Extra = e.position
                });

            var results = customers.Concat(employees).ToList();

            return View(results);
        }

        // Optional error page
        // [AllowAnonymous]
        // public IActionResult Error()
        // {
        //     return View();
        // }
    }
}
