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
    public class UsuariosController : Controller
    {
        private readonly FomezeroContext _context;

        public UsuariosController(FomezeroContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.TipoUsuario) // Inclui o relacionamento com TipoUsuario
                .ToListAsync();
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            // Preenche o dropdown com os tipos de usuário disponíveis
            ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone,Senha,TipoUsuarioId,DocumentoIdentificacao")] Usuario usuario, string DescricaoTipoUsuario)
        {
            // Removendo a validação do campo "DescricaoTipoUsuario" do ModelState
            ModelState.Remove("DescricaoTipoUsuario");

            // Verifica se o email já existe no banco de dados
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "O email já está em uso. Por favor, insira um email diferente.");
            }

            if (!ModelState.IsValid)
            {
                // Logando os erros do ModelState para entender o problema de validação
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Erro: {error.ErrorMessage}");
                    }
                }

                // Recarrega a lista de opções caso haja erro no ModelState
                ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
                return View(usuario);
            }

            try
            {
                // Recupera o TipoUsuario do banco de dados para popular a propriedade de navegação
                usuario.TipoUsuario = await _context.TipoUsuarios.FindAsync(usuario.TipoUsuarioId);
                if (usuario.TipoUsuario == null)
                {
                    ModelState.AddModelError("TipoUsuarioId", "O tipo de usuário selecionado é inválido.");
                    ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
                    return View(usuario);
                }

                // Log do valor de TipoUsuarioId e da descrição do tipo
                Console.WriteLine($"TipoUsuarioId recebido: {usuario.TipoUsuarioId}, Descrição: {usuario.TipoUsuario.Descricao}");

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Loga o erro ocorrido durante o salvamento do usuário
                Console.WriteLine($"Erro ao salvar usuário: {ex.Message}");
            }

            // Recarrega a lista de opções para o dropdown caso algo dê errado
            ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Preenche o dropdown com os tipos de usuário disponíveis para a edição
            ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // POST: Editar USUÁRIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone,Senha,TipoUsuarioId,DocumentoIdentificacao")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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

            // Recarrega a lista de opções caso haja erro no ModelState
            ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Confirmar Exclusão de USUÁRIO
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Verifica se o usuário existe no banco de dados
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
