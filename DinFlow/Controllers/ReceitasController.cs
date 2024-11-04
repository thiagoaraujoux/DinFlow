using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DinFlow.Models;
using Microsoft.AspNet.Identity;

namespace DinFlow.Controllers
{
    [Authorize]
    public class ReceitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Receitas
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var receitas = db.Receitas.Where(r => r.UserId == userId).Include(r => r.Tags).ToList();
            return View(receitas);
        }

        // GET: Receitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Include(r => r.Tags).FirstOrDefault(r => r.Id == id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // GET: Receitas/Create
        public ActionResult Create()
        {
            ViewBag.Tags = db.Tags.ToList();
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Receitas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Valor,Data,CategoriaId")] Receita receita, int[] SelectedTags)
        {
            if (ModelState.IsValid)
            {
                receita.UserId = User.Identity.GetUserId();
                if (SelectedTags != null)
                {
                    receita.Tags = db.Tags.Where(t => SelectedTags.Contains(t.Id)).ToList();
                }
                db.Receitas.Add(receita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tags = db.Tags.ToList();
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", receita.CategoriaId);
            return View(receita);
        }

        // GET: Receitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Receita receita = db.Receitas.Include(r => r.Tags).FirstOrDefault(r => r.Id == id);

            if (receita == null)
            {
                return HttpNotFound();
            }

            ViewBag.Tags = db.Tags.ToList();
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", receita.CategoriaId);
            receita.SelectedTags = receita.Tags?.Select(t => t.Id).ToList() ?? new List<int>();

            return View(receita);
        }

        // POST: Receitas/Edit/5
        // POST: Receitas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Valor,Data,CategoriaId,SelectedTags")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                // Obter a receita do banco de dados com as tags associadas
                var receitaFromDb = db.Receitas.Include(r => r.Tags).FirstOrDefault(r => r.Id == receita.Id);

                if (receitaFromDb == null)
                {
                    return HttpNotFound();
                }

                // Atualiza os dados básicos da receita
                receitaFromDb.Nome = receita.Nome;
                receitaFromDb.Descricao = receita.Descricao;
                receitaFromDb.Valor = receita.Valor;
                receitaFromDb.Data = receita.Data;
                receitaFromDb.CategoriaId = receita.CategoriaId;

                // Atualiza as tags
                var selectedTags = receita.SelectedTags ?? new List<int>();
                var currentTags = receitaFromDb.Tags.Select(t => t.Id).ToList();

                // Remover tags que não estão mais selecionadas
                foreach (var tag in receitaFromDb.Tags.ToList())
                {
                    if (!selectedTags.Contains(tag.Id))
                    {
                        receitaFromDb.Tags.Remove(tag);
                    }
                }

                // Adicionar novas tags selecionadas
                foreach (var tagId in selectedTags)
                {
                    if (!currentTags.Contains(tagId))
                    {
                        var tagToAdd = db.Tags.Find(tagId);
                        if (tagToAdd != null)
                        {
                            receitaFromDb.Tags.Add(tagToAdd);
                        }
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tags = db.Tags.ToList();
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", receita.CategoriaId);
            return View(receita);
        }

        // GET: Receitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // POST: Receitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receita receita = db.Receitas.Find(id);
            db.Receitas.Remove(receita);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
