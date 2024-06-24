using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication1.Validations;

namespace WebApplication1.Model
{
    [Table("Produtos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "O nome deve ter entre 5 a 20 caracteres",
            MinimumLength = 5)]
        [PrimeiraLetraMaiuscula]
        public string? Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string? Descricao { get; set; }
        [Required]
        [Range(1, 10000, ErrorMessage = "O preco deve ester entre {1} a {2} reais")]
        public decimal Preco { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraLetra = this.Nome[0].ToString();
                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new
                        ValidationResult("A primeira letra deve ser maiuscula",
                        new[] { nameof(this.Nome) });
                }
            }

            if (this.Estoque <= 0)
            {
                yield return new
                        ValidationResult("O Estoque deve ser maior que 0",
                        new[] { nameof(this.Nome) });
            }
        }
    }
}
