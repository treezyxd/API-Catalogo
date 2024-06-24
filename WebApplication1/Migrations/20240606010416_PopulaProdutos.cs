using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, " +
            "DataCadastro, CategoriaId) Values('Coca-Cola Diet', 'Refrigerante de Cola de 350ml', " + 
            "5.45, 'cocacoladiet.jpg', 50, now(), 1)");

            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, " +
            "DataCadastro, CategoriaId) Values('Lanche de Atum', 'Sanduiche de Atum com Maionese', " + 
            "8.50, 'lancheatum.jpg', 10, now(), 2)");

            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, " +
            "DataCadastro, CategoriaId) Values('Pudim', 'Pudim de Leite Condesado de 100g', " + 
            "6.75, 'pudim.jpg', 20, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("");
        }
    }
}
