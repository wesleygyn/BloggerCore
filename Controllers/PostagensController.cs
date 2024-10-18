using BloggerCore.Data;
using BloggerCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace BloggerCore.Controllers
{
    //[Authorize]
    public class PostagensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostagensController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(Postagem novaPostagem)
        {
            if (ModelState.IsValid)
            {
                novaPostagem.DataAlteracao = DateTime.Now;
                novaPostagem.Autor = User.Identity?.Name;
                novaPostagem.Operacao = "Postado";
                _context.Add(novaPostagem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(novaPostagem);
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var result = _context.postagens.Find(id);
            return View(result);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.postagens == null)
            {
                return NotFound();
            }

            var postagemEdit = await _context.postagens.FindAsync(id);
            if (postagemEdit == null)
            {
                return NotFound();
            }

            postagemEdit.Descricao = HttpUtility.HtmlDecode(postagemEdit.Descricao);
            return View(postagemEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Postagem postagem)
        {
            if (id != postagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    postagem.DataAlteracao = DateTime.Now;
                    postagem.Autor = User.Identity?.Name;
                    postagem.Operacao = "Editado";
                    _context.Update(postagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostagemExists(postagem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(postagem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.postagens == null)
            {
                return NotFound();
            }

            var profitsDocs = await _context.postagens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profitsDocs == null)
            {
                return NotFound();
            }

            return View(profitsDocs);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.postagens == null)
            {
                return Problem("A postagem {id} não existe!");
            }
            var profitsDocs = await _context.postagens.FindAsync(id);
            if (profitsDocs != null)
            {
                _context.postagens.Remove(profitsDocs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool PostagemExists(int id)
        {
            return _context.postagens.Any(e => e.Id == id);
        }

    }
}