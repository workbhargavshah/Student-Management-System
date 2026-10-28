
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using System.Diagnostics;

namespace StudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .AsNoTracking()
                .ToListAsync();

            ViewBag.TotalStudents = students.Count;
            ViewBag.TotalDepartments = students
                .Select(s => s.Department)
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .Distinct()
                .Count();

            ViewBag.TotalSemesters = students
                .Select(s => s.Semester)
                .Distinct()
                .Count();

            ViewBag.TotalRecords = students.Count;

            ViewBag.CSECount = students.Count(s => s.Department == "CSE");
            ViewBag.ITCount = students.Count(s => s.Department == "IT");
            ViewBag.CECount = students.Count(s => s.Department == "CE");
            ViewBag.OtherCount = students.Count(s =>
                s.Department != "CSE" &&
                s.Department != "IT" &&
                s.Department != "CE");

            ViewBag.RecentStudents = students
                .OrderByDescending(s => s.Id)
                .Take(5)
                .ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
