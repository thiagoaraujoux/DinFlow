using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DinFlow.Models
{
    // Classe ApplicationUser, onde podem ser adicionados dados personalizados do usuário
    public class ApplicationUser : IdentityUser
    {
        // Método para gerar o ClaimsIdentity, que gerencia as claims do usuário para autenticação
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
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
