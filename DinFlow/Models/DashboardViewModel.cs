using System;
using System.Collections.Generic;
using System.Linq;

namespace DinFlow.Models
{
    public class DashboardViewModel
    {
        public int CategoriaId { get; set; }

        // Dynamically calculated properties for totals
        public decimal TotalReceitas { get; set; } // Remove 'readonly'
        public decimal TotalDespesas { get; set; } // Remove 'readonly'
        public decimal TotalEconomias { get; set; }

        // Renamed classes to avoid ambiguity
        public List<ReceitaDetalhe> Receitas { get; set; }
        public List<DespesaDetalhe> Despesas { get; set; }
        public List<EconomiaDetalhe> Economias { get; set; }

        // Constructor to initialize lists
        public DashboardViewModel()
        {
            Receitas = new List<ReceitaDetalhe>();
            Despesas = new List<DespesaDetalhe>();
            Economias = new List<EconomiaDetalhe>();
        }
    }

    public class ReceitaDetalhe
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }

    public class DespesaDetalhe
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }

    public class EconomiaDetalhe
    {
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }
}
