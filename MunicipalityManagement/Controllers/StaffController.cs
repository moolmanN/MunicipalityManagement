namespace MunicipalityManagement;
using System;
using Microsoft.AspNetCore.Mvc;
using MunicipalityManagement.Data;
using MunicipalityManagement.Models;

public class StaffController : Controller
{
    private readonly AppDbContext _context;

    public StaffController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var staff = _context.Staffs.ToList();
        return View(staff);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Staff staff)
    {
        if (ModelState.IsValid)
        {
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(staff);
    }

    public IActionResult Edit(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff == null) return NotFound();
        return View(staff);
    }

    [HttpPost]
    public IActionResult Edit(Staff staff)
    {
        if (ModelState.IsValid)
        {
            _context.Staffs.Update(staff);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(staff);
    }

    public IActionResult Details(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff == null) return NotFound();
        return View(staff);
    }

    public IActionResult Delete(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff == null) return NotFound();
        return View(staff);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff != null)
        {
            _context.Staffs.Remove(staff);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
