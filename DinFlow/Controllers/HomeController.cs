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
                // Obtém o ID do usuário autenticado
                var userId = User.Identity.GetUserId();

                // Cálculo dos totais, tratando valores nulos e filtrando por UserId
                var totalReceitas = db.Receitas
                    .Where(r => r.UserId == userId)
                    .Sum(r => (decimal?)r.Valor) ?? 0m; // Utilize 0m para um decimal

                var totalDespesas = db.Despesas
                    .Where(d => d.UserId == userId)
                    .Sum(d => (decimal?)d.Valor) ?? 0m; // Utilize 0m para um decimal

                var totalEconomias = db.Economias
                    .Where(e => e.UserId == userId)
                    .Sum(e => (decimal?)e.Valor) ?? 0m; // Utilize 0m para um decimal

                // Preenche as listas de detalhes
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
                    .Select(e => new EconomiaDetalhe { Valor = e.Valor })
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

                return View(model);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
