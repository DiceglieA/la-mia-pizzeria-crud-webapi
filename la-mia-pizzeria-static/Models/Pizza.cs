using la_mia_pizzeria_static.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Elemento non trovato")]
        [StringLength(25, ErrorMessage = "Il nome non puo avere piu di 25 caratteri")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Elemento non trovato")]
        [Column(TypeName = "text")]
        [MoreThenAWorld(5)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Elemento non trovato")]
        public string Foto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obbligatorio")]
        [MoreThenZeroPrice]
        public int Price { get; set; }

        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public List<Ingrediente> Ingredients { get; set; }
        public Pizza()
        {
            this.Ingredients = new List<Ingrediente>();
        }
    }
}
