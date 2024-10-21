﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DinFlow.Models
{
    // Classe ApplicationUser, onde podem ser adicionados dados personalizados do usuário
    public class ApplicationUser : IdentityUser
    {
        // Método para gerar o ClaimsIdentity, que gerencia as claims do usuário para autenticação
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que a authenticationType deve corresponder a uma definida em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações do usuário personalizadas aqui
            return userIdentity;
        }

        // Relacionamentos com outras entidades
        public virtual ICollection<Receita> Receitas { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
    }

    // Contexto da aplicação, herdando de IdentityDbContext para gerenciar Identity e suas funcionalidades
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Método de criação do contexto
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // DbSets das demais entidades do sistema
        public DbSet<Economia> Economias { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}