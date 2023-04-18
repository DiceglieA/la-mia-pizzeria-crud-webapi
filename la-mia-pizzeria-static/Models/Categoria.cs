using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Il nome della categoria è obbligatorio")]
        [StringLength(40, ErrorMessage = "Il nome non deve superare 40 caratteri")]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Pizza>? Pizzas { get; set; }
    }
}
