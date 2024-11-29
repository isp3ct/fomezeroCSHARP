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
            // Verifica se o ID do usuário está armazenado na sessão
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            string? tipoUsuario = HttpContext.Session.GetString("TipoUsuarioId");

            IQueryable<Doacao> DoacoesQuery = _context.Doacoes
                .Include(d => d.TipoDoacao)
                .Include(d => d.Usuario);

            if (usuarioId.HasValue && tipoUsuario != "Admin")
            {
                // Filtra apenas as doações do usuário logado se não for administrador
                DoacoesQuery = DoacoesQuery.Where(d => d.UsuarioId == usuarioId.Value);
            }

            // Retorna a lista de doações (filtrada ou completa)
            return View(await DoacoesQuery.ToListAsync());
        }

        // GET: Doacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Doacao = await _context.Doacoes
                .Include(d => d.TipoDoacao)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Doacao == null)
            {
                return NotFound();
            }

            return View(Doacao);
        }

        // GET: Doacoes/Create
        public IActionResult Create()
        {
            ViewData["TipoDoacaoDescricao"] = new SelectList(_context.TipoDoacaos, "Id", "Descricao");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email");
            return View();
        }

        // POST: Doacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,TipoDoacaoId,Valor,DescricaoAlimento,DataDoacao")] Doacao Doacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Doacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoDoacaoId"] = new SelectList(_context.TipoDoacaos, "Id", "Id", Doacao.TipoDoacaoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", Doacao.UsuarioId);
            return View(Doacao);
        }

        // GET: Doacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Doacao = await _context.Doacoes.FindAsync(id);
            if (Doacao == null)
            {
                return NotFound();
            }
            ViewData["TipoDoacaoDescricao"] = new SelectList(_context.TipoDoacaos, "Id", "Descricao", Doacao.TipoDoacaoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", Doacao.UsuarioId);
            return View(Doacao);
        }

        // POST: Doacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,TipoDoacaoId,Valor,DescricaoAlimento,DataDoacao")] Doacao Doacao)
        {
            if (id != Doacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Doacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoacaoExists(Doacao.Id))
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
            ViewData["TipoDoacaoId"] = new SelectList(_context.TipoDoacaos, "Id", "Id", Doacao.TipoDoacaoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", Doacao.UsuarioId);
            return View(Doacao);
        }

        // GET: Doacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Doacao = await _context.Doacoes
                .Include(d => d.TipoDoacao)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Doacao == null)
            {
                return NotFound();
            }

            return View(Doacao);
        }

        // POST: Doacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Doacao = await _context.Doacoes.FindAsync(id);
            if (Doacao != null)
            {
                _context.Doacoes.Remove(Doacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoacaoExists(int id)
        {
            return _context.Doacoes.Any(e => e.Id == id);
        }
    }
}
