using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloggerCore.Data;
using BloggerCore.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BloggerCore.Controllers
{
    public class PessoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            var pessoas = await _context.pessoas.ToListAsync();

              return View(pessoas);
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.pessoas == null)
            {
                return NotFound();
            }

            var pessoa = await _context.pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // POST: Pessoas/Update
        [HttpPost]
        public IActionResult Update([DataSourceRequest] DataSourceRequest request, Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Log para verificar o valor da data antes de atualizar
                    Console.WriteLine($"Atualizando pessoa: {pessoa.Id}, Nascimento: {pessoa.Nascimento}");

                    _context.Update(pessoa);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError("", $"Erro ao atualizar registro: {ex.Message}");
                }
            }

            return Json(new[] { pessoa }.ToDataSourceResult(request, ModelState));
        }





        //[HttpPost]
        //public IActionResult Update([DataSourceRequest] DataSourceRequest request, Pessoa pessoa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(pessoa);  // Atualizando o modelo no banco de dados
        //            _context.SaveChanges();  // Salvando as alterações
        //        }
        //        catch (DbUpdateConcurrencyException ex)
        //        {
        //            // Lógica de tratamento para concorrência (caso o registro tenha sido alterado por outra pessoa)
        //            ModelState.AddModelError("", $"Erro ao atualizar registro: {ex.Message}");
        //        }
        //    }

        //    // Retornando o resultado para a Grid do Kendo
        //    return Json(new[] { pessoa }.ToDataSourceResult(request, ModelState));
        //}

        // POST: Pessoas/Delete
        [HttpPost]
        public IActionResult Delete([DataSourceRequest] DataSourceRequest request, Pessoa pessoa)
        {
            if (pessoa != null)
            {
                var pessoaToDelete = _context.pessoas.Find(pessoa.Id);
                if (pessoaToDelete != null)
                {
                    _context.pessoas.Remove(pessoaToDelete);
                    _context.SaveChanges();
                }
            }

            return Json(new[] { pessoa }.ToDataSourceResult(request, ModelState));
        }

        private bool PessoaExists(int id)
        {
          return _context.pessoas.Any(e => e.Id == id);
        }

        public JsonResult ReadData([DataSourceRequest] DataSourceRequest request)
        {
            var pessoas = _context.pessoas.ToList();
            return Json(pessoas.ToDataSourceResult(request));
        }
    }
}