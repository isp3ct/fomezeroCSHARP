using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fomezero.Models;
using Microsoft.AspNetCore.Http;

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
            // Obtém a lista de usuários, incluindo o TipoUsuario
            var usuarios = await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .ToListAsync();

            // Retorna a View com o modelo correto
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            // Filtra para excluir o tipo "Admin" da lista de tipos de usuário
            var tiposUsuario = _context.TipoUsuarios
                .Where(t => t.Descricao != "Admin") // Exclui o tipo 'Admin'
                .ToList();

            ViewBag.TipoUsuarioId = new SelectList(tiposUsuario, "Id", "Descricao");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,Telefone,Senha,TipoUsuarioId,DocumentoIdentificacao")] Usuario usuario)
        {
            // Verifica se o email já existe no banco de dados
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "O email já está em uso. Por favor, insira um email diferente.");
            }

            // Verifica se o CPF já existe no banco de dados
            if (_context.Usuarios.Any(u => u.DocumentoIdentificacao == usuario.DocumentoIdentificacao))
            {
                ModelState.AddModelError("DocumentoIdentificacao", "O CPF já está em uso. Por favor, insira um CPF diferente.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
                return View(usuario);
            }

            try
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return View("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar usuário: {ex.Message}");
                ModelState.AddModelError("", "Ocorreu um erro ao salvar os dados. Por favor, tente novamente.");
            }

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

            ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone,Senha,TipoUsuarioId,DocumentoIdentificacao")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            // Verifica se o email já está em uso por outro usuário
            if (_context.Usuarios.Any(u => u.Email == usuario.Email && u.Id != id))
            {
                ModelState.AddModelError("Email", "O email já está em uso. Por favor, insira um email diferente.");
            }

            // Verifica se o CPF já está em uso por outro usuário
            if (_context.Usuarios.Any(u => u.DocumentoIdentificacao == usuario.DocumentoIdentificacao && u.Id != id))
            {
                ModelState.AddModelError("DocumentoIdentificacao", "O CPF já está em uso. Por favor, insira um CPF diferente.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.TipoUsuarioId = new SelectList(_context.TipoUsuarios.ToList(), "Id", "Descricao", usuario.TipoUsuarioId);
                return View(usuario);
            }

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EfetuarLogin(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ModelState.AddModelError(string.Empty, "Email e Senha são obrigatórios.");
                return View("Login");
            }

            // Verifica se o email e senha correspondem a um usuário no banco de dados
            var usuario = await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Email ou senha incorretos.");
                return View("Login");
            }

            // Armazena o ID do usuário e o tipo de usuário na sessão após o login bem-sucedido
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetString("TipoUsuarioId", usuario.TipoUsuario?.Descricao ?? ""); // Salva a descrição do tipo de usuário

            // Redireciona para a página Index ou alguma outra página específica após o login bem-sucedido
            return View("../Home/Index");
        }

        // GET: Usuarios/Logout
        public IActionResult Logout()
        {
            // Limpa a sessão
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
