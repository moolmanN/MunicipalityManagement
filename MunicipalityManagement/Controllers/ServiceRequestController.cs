using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagement.Data;
using MunicipalityManagement.Models;
using System.Linq;

namespace MunicipalityManagement.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceRequestController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var requests = _context.ServiceRequests.Include(r => r.Citizen).ToList();
            return View(requests);
        }

        public IActionResult Create()
        {
            // Populate the dropdown list with citizens.
            ViewBag.Citizens = new SelectList(_context.Citizens, "CitizenID", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.ServiceRequests.Add(request);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            // Repopulate the dropdown if there's an error.
            ViewBag.Citizens = new SelectList(_context.Citizens, "CitizenID", "FullName");
            return View(request);
        }

        public IActionResult UpdateStatus(int id)
        {
            var request = _context.ServiceRequests.Find(id);
            if (request == null)
                return NotFound();
            request.Status = "Resolved";
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var request = _context.ServiceRequests.Find(id);
            if (request == null)
                return NotFound();
            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var request = _context.ServiceRequests.Find(id);
            if (request != null)
            {
                _context.ServiceRequests.Remove(request);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
