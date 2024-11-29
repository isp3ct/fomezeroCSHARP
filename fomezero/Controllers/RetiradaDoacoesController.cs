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
            // Obtém o TipoUsuarioId e UsuarioId da sessão
            var tipoUsuarioId = HttpContext.Session.GetString("TipoUsuarioId");
            var userId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            IQueryable<RetiradaDoacao> fomezeroContext = _context.RetiradaDoacoes
                .Include(r => r.Beneficiario)
                .Include(r => r.Doacao)
                .Include(r => r.LocalRetirada);

            // Se não for admin, mostra apenas as retiradas do usuário logado
            if (tipoUsuarioId != "1" && userId != 0)
            {
                fomezeroContext = fomezeroContext.Where(r => r.BeneficiarioId == userId);
            }

            return View(await fomezeroContext.ToListAsync());
        }

        // GET: RetiradaDoacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retiradaDoacao = await _context.RetiradaDoacoes
                .Include(r => r.Beneficiario)
                .Include(r => r.Doacao)
                .Include(r => r.LocalRetirada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retiradaDoacao == null)
            {
                return NotFound();
            }

            return View(retiradaDoacao);
        }

        // GET: RetiradaDoacoes/Create
        public IActionResult Create()
        {
            // Obtém o UsuarioId da sessão
            var userId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            // Se o usuário não for admin, definir automaticamente o BeneficiarioId para o usuário logado
            var doacoesNaoRetiradas = _context.Doacoes
                .Where(d => !_context.RetiradaDoacoes.Any(r => r.DoacaoId == d.Id));

            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios.Where(u => u.Id == userId), "Id", "Nome");
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(doacoesNaoRetiradas, "Id", "DescricaoAlimento");
            ViewData["LocalRetiradaEndereco"] = new SelectList(_context.LocaisRetirada, "Id", "Endereco");
            return View();
        }

        // POST: RetiradaDoacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoacaoId,BeneficiarioId,LocalRetiradaId,DataAgendada,DataRetirada")] RetiradaDoacao retiradaDoacao)
        {
            if (ModelState.IsValid)
            {
                // Obtém o UsuarioId da sessão e define o BeneficiarioId
                var userId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
                if (userId != 0)
                {
                    retiradaDoacao.BeneficiarioId = userId;
                }

                _context.Add(retiradaDoacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", retiradaDoacao.BeneficiarioId);
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(_context.Doacoes, "Id", "DescricaoAlimento", retiradaDoacao.DoacaoId);
            ViewData["LocalRetiradaEndereco"] = new SelectList(_context.LocaisRetirada, "Id", "Endereco", retiradaDoacao.LocalRetiradaId);
            return View(retiradaDoacao);
        }

        // GET: RetiradaDoacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retiradaDoacao = await _context.RetiradaDoacoes.FindAsync(id);
            if (retiradaDoacao == null)
            {
                return NotFound();
            }

            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", retiradaDoacao.BeneficiarioId);
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(_context.Doacoes, "Id", "DescricaoAlimento", retiradaDoacao.DoacaoId);
            ViewData["LocalRetiradaEndereco"] = new SelectList(_context.LocaisRetirada, "Id", "Endereco", retiradaDoacao.LocalRetiradaId);
            return View(retiradaDoacao);
        }

        // POST: RetiradaDoacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoacaoId,BeneficiarioId,LocalRetiradaId,DataAgendada,DataRetirada")] RetiradaDoacao retiradaDoacao)
        {
            if (id != retiradaDoacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retiradaDoacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetiradaDoacaoExists(retiradaDoacao.Id))
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

            ViewData["BeneficiarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", retiradaDoacao.BeneficiarioId);
            ViewData["DoacaoDescricaoAlimento"] = new SelectList(_context.Doacoes, "Id", "DescricaoAlimento", retiradaDoacao.DoacaoId);
            ViewData["LocalRetiradaEndereco"] = new SelectList(_context.LocaisRetirada, "Id", "Endereco", retiradaDoacao.LocalRetiradaId);
            return View(retiradaDoacao);
        }

        // GET: RetiradaDoacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retiradaDoacao = await _context.RetiradaDoacoes
                .Include(r => r.Beneficiario)
                .Include(r => r.Doacao)
                .Include(r => r.LocalRetirada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retiradaDoacao == null)
            {
                return NotFound();
            }

            return View(retiradaDoacao);
        }

        // POST: RetiradaDoacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var retiradaDoacao = await _context.RetiradaDoacoes.FindAsync(id);
            if (retiradaDoacao != null)
            {
                _context.RetiradaDoacoes.Remove(retiradaDoacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetiradaDoacaoExists(int id)
        {
            return _context.RetiradaDoacoes.Any(e => e.Id == id);
        }
    }
}
