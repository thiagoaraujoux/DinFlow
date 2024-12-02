namespace DinF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Despesas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receitas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Economias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TagDespesas",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Despesa_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Despesa_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Despesas", t => t.Despesa_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Despesa_Id);
            
            CreateTable(
                "dbo.ReceitaTags",
                c => new
                    {
                        Receita_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Receita_Id, t.Tag_Id })
                .ForeignKey("dbo.Receitas", t => t.Receita_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Receita_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Economias", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Despesas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Receitas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReceitaTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.ReceitaTags", "Receita_Id", "dbo.Receitas");
            DropForeignKey("dbo.Receitas", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.TagDespesas", "Despesa_Id", "dbo.Despesas");
            DropForeignKey("dbo.TagDespesas", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Despesas", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.ReceitaTags", new[] { "Tag_Id" });
            DropIndex("dbo.ReceitaTags", new[] { "Receita_Id" });
            DropIndex("dbo.TagDespesas", new[] { "Despesa_Id" });
            DropIndex("dbo.TagDespesas", new[] { "Tag_Id" });
            DropIndex("dbo.Economias", new[] { "UserId" });
            DropIndex("dbo.Receitas", new[] { "CategoriaId" });
            DropIndex("dbo.Receitas", new[] { "UserId" });
            DropIndex("dbo.Despesas", new[] { "CategoriaId" });
            DropIndex("dbo.Despesas", new[] { "UserId" });
            DropTable("dbo.ReceitaTags");
            DropTable("dbo.TagDespesas");
            DropTable("dbo.Economias");
            DropTable("dbo.Receitas");
            DropTable("dbo.Tags");
            DropTable("dbo.Despesas");
            DropTable("dbo.Categorias");
        }
    }
}
