using System;
using System.Collections.Generic;
using System.Linq;

namespace DinFlow.Models
{
    public class DashboardViewModel
    {
        public int CategoriaId { get; set; }

        // Dynamically calculated properties for totals
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal TotalEconomias { get; set; }

        // Renamed classes to avoid ambiguity
        public List<Movimentacao> UltimasMovimentacoes { get; set; } = new List<Movimentacao>();
        public List<CategoriaRanking> RankingCategorias { get; set; } = new List<CategoriaRanking>();
        public List<ReceitaDetalhe> Receitas { get; set; }
        public List<DespesaDetalhe> Despesas { get; set; }
        public List<EconomiaDetalhe> Economias { get; set; }
        public List<string> Tags { get; set; }

        // Constructor to initialize lists
        public DashboardViewModel()
        {
            Receitas = new List<ReceitaDetalhe>();
            Despesas = new List<DespesaDetalhe>();
            Economias = new List<EconomiaDetalhe>();
        }

        // Método para compilar e ordenar as últimas movimentações
        public void AtualizarUltimasMovimentacoes()
        {
            var movimentacoes = new List<Movimentacao>();

            // Adiciona receitas às movimentações
            movimentacoes.AddRange(Receitas.Select(r => new Movimentacao
            {
                Valor = r.Valor,
                Descricao = r.Descricao,
                Data = DateTime.Now // Use a data de criação ou a data da receita se disponível
            }));

            // Adiciona despesas às movimentações
            movimentacoes.AddRange(Despesas.Select(d => new Movimentacao
            {
                Valor = -d.Valor, // Negativo para representar despesa
                Descricao = d.Descricao,
                Data = DateTime.Now // Use a data de criação ou a data da despesa se disponível
            }));

            // Ordena as movimentações por data (a mais recente primeiro)
            UltimasMovimentacoes = movimentacoes.OrderByDescending(m => m.Data).ToList();
        }
    }

    public class ReceitaDetalhe
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; } // Adicione esta propriedade se precisar
    }

    public class DespesaDetalhe
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; } // Adicione esta propriedade se precisar
    }

    public class EconomiaDetalhe
    {
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }

    public class Movimentacao
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }

    public class CategoriaRanking
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
