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
    public class DoacoesInstituicoesController : Controller
    {
        private readonly FomezeroContext _context;

        public DoacoesInstituicoesController(FomezeroContext context)
        {
            _context = context;
        }

        // GET: DoacoesInstituicoes
        public async Task<IActionResult> Index()
        {
            var fomezeroContext = _context.DoacoesInstituicoes.Include(d => d.Doacao).Include(d => d.Instituicao);
            return View(await fomezeroContext.ToListAsync());
        }

        // GET: DoacoesInstituicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DoacoesInstituico = await _context.DoacoesInstituicoes
                .Include(d => d.Doacao)
                .Include(d => d.Instituicao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (DoacoesInstituico == null)
            {
                return NotFound();
            }

            return View(DoacoesInstituico);
        }

        // GET: DoacoesInstituicoes/Create
        public IActionResult Create()
        {
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(_context.Doacoes, "Id", "DescricaoAlimento");
            ViewData["InstituicaoNome"] = new SelectList(_context.Instituicoes, "Id", "Nome");
            return View();
        }

        // POST: DoacoesInstituicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoacaoId,InstituicaoId")] DoacoesInstituico DoacoesInstituico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(DoacoesInstituico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoacaoId"] = new SelectList(_context.Doacoes, "Id", "Id", DoacoesInstituico.DoacaoId);
            ViewData["InstituicaoId"] = new SelectList(_context.Instituicoes, "Id", "Id", DoacoesInstituico.InstituicaoId);
            return View(DoacoesInstituico);
        }

        // GET: DoacoesInstituicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DoacoesInstituico = await _context.DoacoesInstituicoes.FindAsync(id);
            if (DoacoesInstituico == null)
            {
                return NotFound();
            }
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(_context.Doacoes, "Id", "DescricaoAlimento", DoacoesInstituico.DoacaoId);
            ViewData["InstituicaoNome"] = new SelectList(_context.Instituicoes, "Id", "Nome", DoacoesInstituico.InstituicaoId);
            return View(DoacoesInstituico);
        }

        // POST: DoacoesInstituicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoacaoId,InstituicaoId")] DoacoesInstituico DoacoesInstituico)
        {
            if (id != DoacoesInstituico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(DoacoesInstituico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoacoesInstituicoExists(DoacoesInstituico.Id))
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
            ViewData["DoacaoId"] = new SelectList(_context.Doacoes, "Id", "Id", DoacoesInstituico.DoacaoId);
            ViewData["InstituicaoId"] = new SelectList(_context.Instituicoes, "Id", "Id", DoacoesInstituico.InstituicaoId);
            return View(DoacoesInstituico);
        }

        // GET: DoacoesInstituicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DoacoesInstituico = await _context.DoacoesInstituicoes
                .Include(d => d.Doacao)
                .Include(d => d.Instituicao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (DoacoesInstituico == null)
            {
                return NotFound();
            }

            return View(DoacoesInstituico);
        }

        // POST: DoacoesInstituicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var DoacoesInstituico = await _context.DoacoesInstituicoes.FindAsync(id);
            if (DoacoesInstituico != null)
            {
                _context.DoacoesInstituicoes.Remove(DoacoesInstituico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoacoesInstituicoExists(int id)
        {
            return _context.DoacoesInstituicoes.Any(e => e.Id == id);
        }
    }
}
