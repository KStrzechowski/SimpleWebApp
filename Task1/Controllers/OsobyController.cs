using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Controllers
{
    public class OsobyController : Controller
    {
        private readonly Task1Context _context;

        public OsobyController(Task1Context context)
        {
            _context = context;
        }

        // GET: Osoby
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.FirstNameSortParm = sortOrder == "Imie" ? "Imie_desc" : "Imie";
            ViewBag.LastNameSortParm = sortOrder == "Nazwisko" ? "Nazwisko_desc" : "Nazwisko";
            ViewBag.DateSortParm = sortOrder == "DataProd" ? "DataProd_desc" : "DataProd";
            ViewBag.SamochodIdSortParm = sortOrder == "SamochodId" ? "SamochodId_desc" : "SamochodId";

            var Osoby = from os in _context.Osoby.Include(o => o.Samochod)
                        select os;
            Osoby = sortOrder switch
            {
                "Imie" => Osoby.OrderBy(os => os.Imie),
                "Imie_desc" => Osoby.OrderByDescending(os => os.Imie),
                "Nazwisko" => Osoby.OrderBy(os => os.Nazwisko),
                "Nazwisko_desc" => Osoby.OrderByDescending(os => os.Nazwisko),
                "DataProd" => Osoby.OrderBy(os => os.DataProd),
                "DataProd_desc" => Osoby.OrderByDescending(os => os.DataProd),
                "SamochodId" => Osoby.OrderBy(os => os.SamochodId),
                "SamochodId_desc" => Osoby.OrderByDescending(os => os.SamochodId),
                _ => Osoby.OrderBy(os => os.OsobaId),
            };
            return View(await Osoby.ToListAsync());
        }

        // GET: Osoby/Search
        public IActionResult Search()
        {
            var task1Context = _context.Osoby.Include(o => o.Samochod);
            return View();
        }

        // POST: Osoby/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase, int ItemType)
        {
            var task1Context = _context.Osoby.Include(o => o.Samochod);
            if (ItemType == 1)
            {
                return View("Index", await _context.Osoby.Where(x => x.Nazwisko.Contains(SearchPhrase)).ToListAsync());
            }
            else
            {
                DateTime productionTime = Convert.ToDateTime(SearchPhrase);
                return View("Index", await _context.Osoby.Where(x => x.DataProd == productionTime).ToListAsync());
            }
        }

        // GET: Osoby/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoby
                .Include(o => o.Samochod)
                .FirstOrDefaultAsync(m => m.OsobaId == id);
            if (osoba == null)
            {
                return NotFound();
            }

            return View(osoba);
        }

        // GET: Osoby/Create
        public IActionResult Create()
        {
            ViewData["SamochodId"] = new SelectList(_context.Samochody, "SamochodId", "SamochodId");
            return View();
        }

        // POST: Osoby/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OsobaId,Imie,Nazwisko,SamochodId,DataProd")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osoba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SamochodId"] = new SelectList(_context.Samochody, "SamochodId", "SamochodId", osoba.SamochodId);
            return View(osoba);
        }

        // GET: Osoby/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoby.FindAsync(id);
            if (osoba == null)
            {
                return NotFound();
            }
            ViewData["SamochodId"] = new SelectList(_context.Samochody, "SamochodId", "SamochodId", osoba.SamochodId);
            return View(osoba);
        }

        // POST: Osoby/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OsobaId,Imie,Nazwisko,SamochodId,DataProd")] Osoba osoba)
        {
            if (id != osoba.OsobaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osoba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsobaExists(osoba.OsobaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SamochodId"] = new SelectList(_context.Samochody, "SamochodId", "SamochodId", osoba.SamochodId);
            return View(osoba);
        }

        // GET: Osoby/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoby
                .Include(o => o.Samochod)
                .FirstOrDefaultAsync(m => m.OsobaId == id);
            if (osoba == null)
            {
                return NotFound();
            }

            return View(osoba);
        }

        // POST: Osoby/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osoba = await _context.Osoby.FindAsync(id);
            _context.Osoby.Remove(osoba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsobaExists(int id)
        {
            return _context.Osoby.Any(e => e.OsobaId == id);
        }
    }
}
