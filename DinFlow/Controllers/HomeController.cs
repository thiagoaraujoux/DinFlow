using DinFlow.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic; // Adicione esta diretiva se não estiver presente
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
                var userId = User.Identity.GetUserId();

                // Cálculo dos totais
                var totalReceitas = db.Receitas
                    .Where(r => r.UserId == userId)
                    .Sum(r => (decimal?)r.Valor) ?? 0m;

                var totalDespesas = db.Despesas
                    .Where(d => d.UserId == userId)
                    .Sum(d => (decimal?)d.Valor) ?? 0m;

                var totalEconomias = db.Economias
                    .Where(e => e.UserId == userId)
                    .Sum(e => (decimal?)e.Valor) ?? 0m;

                // Detalhes de receitas e despesas
                var receitasDetalhes = db.Receitas
                    .Where(r => r.UserId == userId)
                    .Select(r => new ReceitaDetalhe
                    {
                        Valor = (decimal)r.Valor,
                        Descricao = r.Descricao,
                        Data = r.Data // Adicione a propriedade Data
                    })
                    .ToList();

                var despesasDetalhes = db.Despesas
                    .Where(d => d.UserId == userId)
                    .Select(d => new DespesaDetalhe
                    {
                        Valor = (decimal)d.Valor,
                        Descricao = d.Descricao,
                        Data = d.Data // Adicione a propriedade Data
                    })
                    .ToList();

                var model = new DashboardViewModel
                {
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    TotalEconomias = totalEconomias,
                    Receitas = receitasDetalhes,
                    Despesas = despesasDetalhes,
                    Economias = db.Economias.Where(e => e.UserId == userId).Select(e => new EconomiaDetalhe { Valor = e.Valor, Data = e.Data }).ToList()
                };

                // Cálculo do ranking de gastos por categoria
                var rankingCategorias = db.Categorias
                    .Select(c => new CategoriaRanking
                    {
                        Nome = c.Nome,
                        Valor = db.Despesas
                            .Where(d => d.CategoriaId == c.Id && d.UserId == userId)
                            .Sum(d => (decimal?)d.Valor) ?? 0m // Soma dos gastos por categoria
                    })
                    .OrderByDescending(c => c.Valor) // Ordena por maior valor
                    .ToList();

                model.RankingCategorias = rankingCategorias;

                // Atualiza as últimas movimentações
                model.AtualizarUltimasMovimentacoes();

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
            using (var db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();

                var totalReceitas = db.Receitas
                    .Where(r => r.UserId == userId)
                    .Sum(r => (decimal?)r.Valor) ?? 0m;

                var totalDespesas = db.Despesas
                    .Where(d => d.UserId == userId)
                    .Sum(d => (decimal?)d.Valor) ?? 0m;

                var totalEconomias = db.Economias
                    .Where(e => e.UserId == userId)
                    .Sum(e => (decimal?)e.Valor) ?? 0m;

                var receitasDetalhes = db.Receitas
                    .Where(r => r.UserId == userId)
                    .Select(r => new ReceitaDetalhe
                    {
                        Valor = (decimal)r.Valor, // Acesse diretamente se 'Valor' não é nullable
                        Descricao = r.Descricao
                    })
                    .ToList();

                var despesasDetalhes = db.Despesas
                    .Where(d => d.UserId == userId)
                    .Select(d => new DespesaDetalhe
                    {
                        Valor = (decimal)d.Valor, // Acesse diretamente se 'Valor' não é nullable
                        Descricao = d.Descricao
                    })
                    .ToList();

                var economiasDetalhes = db.Economias
                    .Where(e => e.UserId == userId)
                    .Select(e => new EconomiaDetalhe
                    {
                        Valor = e.Valor, // Acesse diretamente se 'Valor' não é nullable
                        Data = e.Data
                    })
                    .ToList();

                var model = new DashboardViewModel
                {
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    TotalEconomias = totalEconomias,
                    Receitas = receitasDetalhes,
                    Despesas = despesasDetalhes,
                    Economias = economiasDetalhes,
                    UltimasMovimentacoes = new List<Movimentacao>() // Inicialize a lista para evitar null
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
