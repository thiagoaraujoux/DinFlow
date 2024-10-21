using DinFlow.Models;
using Microsoft.AspNet.Identity;
using System;
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
                    .Where(r => r.UserId == userId) // Filtra receitas do usuário autenticado
                    .Sum(r => (decimal?)r.Valor) ?? 0;

                var totalDespesas = db.Despesas
                    .Where(d => d.UserId == userId) // Filtra despesas do usuário autenticado
                    .Sum(d => (decimal?)d.Valor) ?? 0;

                var totalEconomias = db.Economias
                    .Where(e => e.UserId == userId) // Filtra economias do usuário autenticado
                    .Sum(e => (decimal?)e.Valor) ?? 0;

                var model = new DashboardViewModel
                {
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    TotalEconomias = totalEconomias
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
