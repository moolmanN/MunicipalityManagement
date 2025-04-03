using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagement.Data;
using MunicipalityManagement.Models;

namespace MunicipalityManagement.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reports/Index
        public IActionResult Index()
        {
            var reports = _context.Reports.Include(r => r.Citizen).ToList();
            return View(reports);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewBag.Citizens = new SelectList(_context.Citizens, "CitizenID", "FullName");
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Reports.Add(report);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Citizens = new SelectList(_context.Citizens, "CitizenID", "FullName");
            return View(report);
        }

        // GET: Reports/Edit/{id}
        public IActionResult Edit(int id)
        {
            var report = _context.Reports.Include(r => r.Citizen).FirstOrDefault(r => r.ReportID == id);
            if (report == null)
            {
                TempData["ErrorMessage"] = "Report not found.";
                return RedirectToAction(nameof(Index));
            }

            var citizens = _context.Citizens.ToList();
            ViewBag.Citizens = citizens.Any()
                ? new SelectList(citizens, "CitizenID", "FullName", report.CitizenID)
                : new SelectList(new List<Citizen>(), "CitizenID", "FullName");

            return View(report);
        }

        // POST: Reports/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Report report)
        {
            if (!ModelState.IsValid)
            {
                var citizens = _context.Citizens.ToList();
                ViewBag.Citizens = citizens.Any()
                    ? new SelectList(citizens, "CitizenID", "FullName", report.CitizenID)
                    : new SelectList(new List<Citizen>(), "CitizenID", "FullName");

                return View(report);
            }

            var existingReport = _context.Reports.Find(report.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found. Unable to update.";
                return RedirectToAction(nameof(Index));
            }

            existingReport.ReportType = report.ReportType;
            existingReport.Details = report.Details;
            existingReport.CitizenID = report.CitizenID;
            existingReport.Status = report.Status;

            _context.Reports.Update(existingReport);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Report updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Reports/ReviewReport/{id}
        public IActionResult ReviewReport(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null) return NotFound();
            report.Status = "Reviewed";
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reports/Delete/{id}
        public IActionResult Delete(int id)
        {
            var report = _context.Reports.Include(r => r.Citizen).FirstOrDefault(r => r.ReportID == id);
            if (report == null) return NotFound();
            return View(report);
        }

        // POST: Reports/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null) return NotFound();

            _context.Reports.Remove(report);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reports/Details/{id}
        public IActionResult Details(int id)
        {
            var report = _context.Reports.Include(r => r.Citizen).FirstOrDefault(r => r.ReportID == id);
            if (report == null) return NotFound();
            return View(report);
        }
    }
}
