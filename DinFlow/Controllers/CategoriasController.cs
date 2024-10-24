using DinFlow.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DinFlow.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            return View(await db.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                // Adiciona uma mensagem de sucesso que expira automaticamente
                TempData["SuccessMessage"] = "Categoria criada com sucesso!";

                return RedirectToAction("Index");
            }

            // Adiciona uma mensagem de erro se a criação falhar
            TempData["ErrorMessage"] = "Erro ao criar a categoria. Verifique os dados fornecidos.";

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                await db.SaveChangesAsync();

                // Adiciona uma mensagem de sucesso que expira automaticamente
                TempData["SuccessMessage"] = "Categoria editada com sucesso!";

                return RedirectToAction("Index");
            }

            // Adiciona uma mensagem de erro se a edição falhar
            TempData["ErrorMessage"] = "Erro ao editar a categoria. Verifique os dados fornecidos.";

            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();

            // Adiciona uma mensagem de sucesso que expira automaticamente
            TempData["SuccessMessage"] = "Categoria excluída com sucesso!";

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

        // POST: Categorias/AdicionarCategoria
        [HttpPost]
        public async Task<JsonResult> AdicionarCategoria(string nomeCategoria)
        {
            if (!string.IsNullOrWhiteSpace(nomeCategoria))
            {
                // Verifica se a categoria já existe
                bool categoriaExiste = await db.Categorias.AnyAsync(c => c.Nome == nomeCategoria);
                if (categoriaExiste)
                {
                    return Json(new { success = false, error = "A categoria já existe." });
                }

                var novaCategoria = new Categoria { Nome = nomeCategoria };
                db.Categorias.Add(novaCategoria);
                await db.SaveChangesAsync();

                // Retorna a nova categoria adicionada
                return Json(new { success = true, categoriaId = novaCategoria.Id, nomeCategoria = novaCategoria.Nome });
            }

            return Json(new { success = false, error = "Nome inválido." });
        }
    }
}
