using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DinFlow.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public virtual ICollection<Receita> Receitas { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
    }
}