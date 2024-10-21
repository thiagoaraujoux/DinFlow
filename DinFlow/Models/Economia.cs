﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DinFlow.Models
{
    public class Economia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}