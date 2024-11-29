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
    public class RetiradaDoacoesController : Controller
    {
        private readonly FomezeroContext _context;

        public RetiradaDoacoesController(FomezeroContext context)
        {
            _context = context;
        }

        // GET: RetiradaDoacoes
        public async Task<IActionResult> Index()
        {
            var fomezeroContext = _context.RetiradaDoacoes.Include(r => r.Beneficiario).Include(r => r.Doacao).Include(r => r.LocalRetirada);
            return View(await fomezeroContext.ToListAsync());
        }

        // GET: RetiradaDoacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retiradaDoaco = await _context.RetiradaDoacoes
                .Include(r => r.Beneficiario)
                .Include(r => r.Doacao)
                .Include(r => r.LocalRetirada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retiradaDoaco == null)
            {
                return NotFound();
            }

            return View(retiradaDoaco);
        }

        // GET: RetiradaDoacoes/Create
        public IActionResult Create()
        {
            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios, "Id", "Email");
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(_context.Doacoes, "Id", "DescricaoAlimento");
            ViewData["LocalRetiradaEndereco"] = new SelectList(_context.LocaisRetirada, "Id", "Endereco");
            return View();
        }

        // POST: RetiradaDoacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoacaoId,BeneficiarioId,LocalRetiradaId,DataAgendada,DataRetirada")] RetiradaDoaco retiradaDoaco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(retiradaDoaco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios, "Id", "Email", retiradaDoaco.BeneficiarioId);
            ViewData["DoacaoId"] = new SelectList(_context.Doacoes, "Id", "Id", retiradaDoaco.DoacaoId);
            ViewData["LocalRetiradaId"] = new SelectList(_context.LocaisRetirada, "Id", "Id", retiradaDoaco.LocalRetiradaId);
            return View(retiradaDoaco);
        }

        // GET: RetiradaDoacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retiradaDoaco = await _context.RetiradaDoacoes.FindAsync(id);
            if (retiradaDoaco == null)
            {
                return NotFound();
            }
            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios, "Id", "Email", retiradaDoaco.BeneficiarioId);
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(_context.Doacoes, "Id", "DescricaoAlimento", retiradaDoaco.DoacaoId);
            ViewData["LocalRetiradaEndereco"] = new SelectList(_context.LocaisRetirada, "Id", "Endereco", retiradaDoaco.LocalRetiradaId);
            return View(retiradaDoaco);
        }

        // POST: RetiradaDoacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoacaoId,BeneficiarioId,LocalRetiradaId,DataAgendada,DataRetirada")] RetiradaDoaco retiradaDoaco)
        {
            if (id != retiradaDoaco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retiradaDoaco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetiradaDoacoExists(retiradaDoaco.Id))
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
            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios, "Id", "Email", retiradaDoaco.BeneficiarioId);
            ViewData["DoacaoId"] = new SelectList(_context.Doacoes, "Id", "Id", retiradaDoaco.DoacaoId);
            ViewData["LocalRetiradaId"] = new SelectList(_context.LocaisRetirada, "Id", "Id", retiradaDoaco.LocalRetiradaId);
            return View(retiradaDoaco);
        }

        // GET: RetiradaDoacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retiradaDoaco = await _context.RetiradaDoacoes
                .Include(r => r.Beneficiario)
                .Include(r => r.Doacao)
                .Include(r => r.LocalRetirada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retiradaDoaco == null)
            {
                return NotFound();
            }

            return View(retiradaDoaco);
        }

        // POST: RetiradaDoacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var retiradaDoaco = await _context.RetiradaDoacoes.FindAsync(id);
            if (retiradaDoaco != null)
            {
                _context.RetiradaDoacoes.Remove(retiradaDoaco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetiradaDoacoExists(int id)
        {
            return _context.RetiradaDoacoes.Any(e => e.Id == id);
        }
    }
}
