using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinFlow.Models
{
    public class Receita
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required]
        public decimal? Valor { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public List<int> SelectedTags { get; set; } 
    }
}