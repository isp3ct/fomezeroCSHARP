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
    public class DoacoesController : Controller
    {
        private readonly FomezeroContext _context;

        public DoacoesController(FomezeroContext context)
        {
            _context = context;
        }

        // GET: Doacoes
        public async Task<IActionResult> Index()
        {
            var fomezeroContext = _context.Doacoes.Include(d => d.TipoDoacao).Include(d => d.Usuario);
            return View(await fomezeroContext.ToListAsync());
        }

        // GET: Doacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doaco = await _context.Doacoes
                .Include(d => d.TipoDoacao)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doaco == null)
            {
                return NotFound();
            }

            return View(doaco);
        }

        // GET: Doacoes/Create
        public IActionResult Create()
        {
            ViewData["TipoDoacaoId"] = new SelectList(_context.TipoDoacaos, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email");
            return View();
        }

        // POST: Doacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,TipoDoacaoId,Valor,DescricaoAlimento,DataDoacao")] Doaco doaco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doaco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoDoacaoId"] = new SelectList(_context.TipoDoacaos, "Id", "Id", doaco.TipoDoacaoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", doaco.UsuarioId);
            return View(doaco);
        }

        // GET: Doacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doaco = await _context.Doacoes.FindAsync(id);
            if (doaco == null)
            {
                return NotFound();
            }
            ViewData["TipoDoacaoId"] = new SelectList(_context.TipoDoacaos, "Id", "Id", doaco.TipoDoacaoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", doaco.UsuarioId);
            return View(doaco);
        }

        // POST: Doacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,TipoDoacaoId,Valor,DescricaoAlimento,DataDoacao")] Doaco doaco)
        {
            if (id != doaco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doaco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoacoExists(doaco.Id))
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
            ViewData["TipoDoacaoId"] = new SelectList(_context.TipoDoacaos, "Id", "Id", doaco.TipoDoacaoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", doaco.UsuarioId);
            return View(doaco);
        }

        // GET: Doacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doaco = await _context.Doacoes
                .Include(d => d.TipoDoacao)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doaco == null)
            {
                return NotFound();
            }

            return View(doaco);
        }

        // POST: Doacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doaco = await _context.Doacoes.FindAsync(id);
            if (doaco != null)
            {
                _context.Doacoes.Remove(doaco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoacoExists(int id)
        {
            return _context.Doacoes.Any(e => e.Id == id);
        }
    }
}
