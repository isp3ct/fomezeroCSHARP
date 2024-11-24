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
    public class InstituicoesController : Controller
    {
        private readonly FomezeroContext _context;

        public InstituicoesController(FomezeroContext context)
        {
            _context = context;
        }

        // GET: Instituicoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instituicoes.ToListAsync());
        }

        // GET: Instituicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituico = await _context.Instituicoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituico == null)
            {
                return NotFound();
            }

            return View(instituico);
        }

        // GET: Instituicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instituicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Contato")] Instituico instituico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instituico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instituico);
        }

        // GET: Instituicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituico = await _context.Instituicoes.FindAsync(id);
            if (instituico == null)
            {
                return NotFound();
            }
            return View(instituico);
        }

        // POST: Instituicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Contato")] Instituico instituico)
        {
            if (id != instituico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituicoExists(instituico.Id))
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
            return View(instituico);
        }

        // GET: Instituicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituico = await _context.Instituicoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituico == null)
            {
                return NotFound();
            }

            return View(instituico);
        }

        // POST: Instituicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instituico = await _context.Instituicoes.FindAsync(id);
            if (instituico != null)
            {
                _context.Instituicoes.Remove(instituico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituicoExists(int id)
        {
            return _context.Instituicoes.Any(e => e.Id == id);
        }
    }
}
