using DinFlow.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace DinFlow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                // Get the authenticated user's ID
                var userId = User.Identity.GetUserId();

                // Calculate totals, handling null values and filtering by UserId
                var totalReceitas = db.Receitas
                    .Where(r => r.UserId == userId)
                    .Sum(r => (decimal?)r.Valor) ?? 0m;
                var totalDespesas = db.Despesas
                    .Where(d => d.UserId == userId)
                    .Sum(d => (decimal?)d.Valor) ?? 0m;
                var totalEconomias = db.Economias
                    .Where(e => e.UserId == userId)
                    .Sum(e => (decimal?)e.Valor) ?? 0m;

                // Populate details lists
                var receitasDetalhes = db.Receitas
                    .Where(r => r.UserId == userId)
                    .Select(r => new ReceitaDetalhe { Valor = (decimal)r.Valor, Descricao = r.Descricao })
                    .ToList();
                var despesasDetalhes = db.Despesas
                    .Where(d => d.UserId == userId)
                    .Select(d => new DespesaDetalhe { Valor = d.Valor, Descricao = d.Descricao })
                    .ToList();
                var economiasDetalhes = db.Economias
                    .Where(e => e.UserId == userId)
                    .Select(e => new EconomiaDetalhe { Valor = e.Valor, Data = e.Data })
                    .ToList();

                var model = new DashboardViewModel
                {
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    TotalEconomias = totalEconomias,
                    Receitas = receitasDetalhes,
                    Despesas = despesasDetalhes,
                    Economias = economiasDetalhes
                };

                // Retrieve categories and pass to view
                ViewBag.Categorias = new SelectList(db.Categorias.ToList(), "Id", "Nome");
                return View(model);
            }
        }

        public ActionResult Relatorio()
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();

                var totalReceitas = db.Receitas.Where(r => r.UserId == userId).Sum(r => (decimal?)r.Valor) ?? 0m;
                var totalDespesas = db.Despesas.Where(d => d.UserId == userId).Sum(d => (decimal?)d.Valor) ?? 0m;
                var totalEconomias = db.Economias.Where(e => e.UserId == userId).Sum(e => (decimal?)e.Valor) ?? 0m;

                var receitasDetalhes = db.Receitas.Where(r => r.UserId == userId).Select(r => new ReceitaDetalhe { Valor = (decimal)r.Valor, Descricao = r.Descricao }).ToList();
                var despesasDetalhes = db.Despesas.Where(d => d.UserId == userId).Select(d => new DespesaDetalhe { Valor = d.Valor, Descricao = d.Descricao }).ToList();
                var economiasDetalhes = db.Economias.Where(e => e.UserId == userId).Select(e => new EconomiaDetalhe { Valor = e.Valor, Data = e.Data }).ToList();

                var model = new DashboardViewModel
                {
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    TotalEconomias = totalEconomias,
                    Receitas = receitasDetalhes,
                    Despesas = despesasDetalhes,
                    Economias = economiasDetalhes
                };

                return View(model);
            }
        }
    }
}
