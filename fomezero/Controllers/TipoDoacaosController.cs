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
    public class TipoDoacaosController : Controller
    {
        private readonly FomezeroContext _context;

        public TipoDoacaosController(FomezeroContext context)
        {
            _context = context;
        }

        // GET: TipoDoacaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDoacaos.ToListAsync());
        }

        // GET: TipoDoacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDoacao = await _context.TipoDoacaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDoacao == null)
            {
                return NotFound();
            }

            return View(tipoDoacao);
        }

        // GET: TipoDoacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDoacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] TipoDoacao tipoDoacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDoacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDoacao);
        }

        // GET: TipoDoacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDoacao = await _context.TipoDoacaos.FindAsync(id);
            if (tipoDoacao == null)
            {
                return NotFound();
            }
            return View(tipoDoacao);
        }

        // POST: TipoDoacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] TipoDoacao tipoDoacao)
        {
            if (id != tipoDoacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDoacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDoacaoExists(tipoDoacao.Id))
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
            return View(tipoDoacao);
        }

        // GET: TipoDoacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDoacao = await _context.TipoDoacaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDoacao == null)
            {
                return NotFound();
            }

            return View(tipoDoacao);
        }

        // POST: TipoDoacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoDoacao = await _context.TipoDoacaos.FindAsync(id);
            if (tipoDoacao != null)
            {
                _context.TipoDoacaos.Remove(tipoDoacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDoacaoExists(int id)
        {
            return _context.TipoDoacaos.Any(e => e.Id == id);
        }
    }
}
