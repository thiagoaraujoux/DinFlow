using DinFlow.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DinFlow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            using (var db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();

                // Cálculo de totais
                var totalReceitas = db.Receitas.Where(r => r.UserId == userId).Sum(r => (decimal?)r.Valor) ?? 0m;
                var totalDespesas = db.Despesas.Where(d => d.UserId == userId).Sum(d => (decimal?)d.Valor) ?? 0m;
                var totalEconomias = db.Economias.Where(e => e.UserId == userId).Sum(e => (decimal?)e.Valor) ?? 0m;

                // Detalhes das receitas e despesas
                var receitasDetalhes = db.Receitas.Where(r => r.UserId == userId)
                    .Select(r => new ReceitaDetalhe
                    {
                        Valor = (decimal)r.Valor,
                        Descricao = r.Descricao,
                        Data = r.Data
                    }).ToList();

                var despesasDetalhes = db.Despesas.Where(d => d.UserId == userId)
                    .Select(d => new DespesaDetalhe
                    {
                        Valor = (decimal)d.Valor,
                        Descricao = d.Descricao,
                        Data = d.Data
                    }).ToList();

                var economiasDetalhes = db.Economias.Where(e => e.UserId == userId)
                    .Select(e => new EconomiaDetalhe
                    {
                        Valor = e.Valor,
                        Data = e.Data
                    }).ToList();

                // Preparar o modelo
                var model = new DashboardViewModel
                {
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    TotalEconomias = totalEconomias,
                    Receitas = receitasDetalhes,
                    Despesas = despesasDetalhes,
                    Economias = economiasDetalhes
                };

                // Cálculo do ranking de categorias
                model.RankingCategorias = db.Categorias.Select(c => new CategoriaRanking
                {
                    Nome = c.Nome,
                    Valor = db.Despesas
                        .Where(d => d.CategoriaId == c.Id && d.UserId == userId)
                        .Sum(d => (decimal?)d.Valor) ?? 0m
                })
                .OrderByDescending(c => c.Valor)
                .ToList();

                // Últimas movimentações
                model.AtualizarUltimasMovimentacoes();

                // Preencher ViewBags para tags e categorias
                ViewBag.Tags = db.Tags.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Nome
                }).ToList();

                ViewBag.Categorias = new SelectList(db.Categorias.ToList(), "Id", "Nome");

                return View(model);
            }
        }

        public ActionResult Relatorio()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            using (var db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();

                var totalReceitas = db.Receitas.Where(r => r.UserId == userId).Sum(r => (decimal?)r.Valor) ?? 0m;
                var totalDespesas = db.Despesas.Where(d => d.UserId == userId).Sum(d => (decimal?)d.Valor) ?? 0m;
                var totalEconomias = db.Economias.Where(e => e.UserId == userId).Sum(e => (decimal?)e.Valor) ?? 0m;

                var receitasDetalhes = db.Receitas.Where(r => r.UserId == userId)
                    .Select(r => new ReceitaDetalhe
                    {
                        Valor = (decimal)r.Valor,
                        Descricao = r.Descricao
                    }).ToList();

                var despesasDetalhes = db.Despesas.Where(d => d.UserId == userId)
                    .Select(d => new DespesaDetalhe
                    {
                        Valor = (decimal)d.Valor,
                        Descricao = d.Descricao
                    }).ToList();

                var economiasDetalhes = db.Economias.Where(e => e.UserId == userId)
                    .Select(e => new EconomiaDetalhe
                    {
                        Valor = e.Valor,
                        Data = e.Data
                    }).ToList();

                var model = new DashboardViewModel
                {
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    TotalEconomias = totalEconomias,
                    Receitas = receitasDetalhes,
                    Despesas = despesasDetalhes,
                    Economias = economiasDetalhes,
                    UltimasMovimentacoes = new List<Movimentacao>() // Evitar null
                };

                model.AtualizarUltimasMovimentacoes();

                return View(model);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de contato.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Página sobre.";
            return View();
        }
    }
}
