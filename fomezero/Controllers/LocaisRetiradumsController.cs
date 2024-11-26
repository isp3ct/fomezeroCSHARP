using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fomezero.Models;

namespace fomezero.Controllers
{
    public class LocaisRetiradumsController : Controller
    {
        private readonly FomezeroContext _context;

        public LocaisRetiradumsController(FomezeroContext context)
        {
            _context = context;
        }

        // GET: LocaisRetiradums
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocaisRetirada.ToListAsync());
        }

        // GET: LocaisRetiradums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaisRetiradum = await _context.LocaisRetirada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locaisRetiradum == null)
            {
                return NotFound();
            }

            return View(locaisRetiradum);
        }

        // GET: LocaisRetiradums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocaisRetiradums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,HorarioDisponivel")] LocaisRetiradum locaisRetiradum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locaisRetiradum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locaisRetiradum);
        }

        // GET: LocaisRetiradums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaisRetiradum = await _context.LocaisRetirada.FindAsync(id);
            if (locaisRetiradum == null)
            {
                return NotFound();
            }
            return View(locaisRetiradum);
        }

        // POST: LocaisRetiradums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,HorarioDisponivel")] LocaisRetiradum locaisRetiradum)
        {
            if (id != locaisRetiradum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locaisRetiradum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocaisRetiradumExists(locaisRetiradum.Id))
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
            return View(locaisRetiradum);
        }

        // GET: LocaisRetiradums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaisRetiradum = await _context.LocaisRetirada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locaisRetiradum == null)
            {
                return NotFound();
            }

            return View(locaisRetiradum);
        }

        // POST: LocaisRetiradums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locaisRetiradum = await _context.LocaisRetirada.FindAsync(id);
            if (locaisRetiradum != null)
            {
                _context.LocaisRetirada.Remove(locaisRetiradum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocaisRetiradumExists(int id)
        {
            return _context.LocaisRetirada.Any(e => e.Id == id);
        }
    }
}
