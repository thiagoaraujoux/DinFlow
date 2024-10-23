using System;
using System.Collections.Generic;

namespace DinFlow.Models
{
    public class DashboardViewModel
    {
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal TotalEconomias { get; set; }

        // Renomeando as classes para evitar ambiguidade
        public List<ReceitaDetalhe> Receitas { get; set; }
        public List<DespesaDetalhe> Despesas { get; set; }
        public List<EconomiaDetalhe> Economias { get; set; }
        public int CategoriaId { get; set; }
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