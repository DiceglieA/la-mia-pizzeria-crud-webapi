using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Categoria>? Categories { get; set; }
        public DbSet<Ingrediente>? Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzaDb;Integrated Security=True;Pooling=False;Encrypt=False;");
        }
        public void seed()
        {
            var pizzaseed = new Pizza[]
            {
                new Pizza
                {
                    Name = "Marinara",
                    Description = "pomodoro, origano, aglio ed olio",
                    Foto = "/img/pizza01.webp",
                    Price = 4,
                },
                new Pizza
                {
                    Name = "Margherita",
                    Description = "pomodoro e mozzarella",
                    Foto = "/img/pizza02.jpg",
                    Price = 5,
                },
                new Pizza
                {
                    Name = "Calabrese",
                    Description = "pomodoro, mozzarella e salame piccante",
                    Foto ="/img/pizza03.jpg",
                    Price = 6,
                },
                new Pizza
                {
                    Name = "Mortadella e pistacchi",
                    Description = "pomodoro, mozzarella, mortadella e pistacchi",
                    Foto = "/img/pizza04.webp",
                    Price = 8,
                },
                new Pizza
                {
                    Name = "Romana",
                    Description = "pomodoro, mozzarella, acciughe, capperi, olive e origano",
                    Foto = "/img/pizza05.jpg",
                    Price = 7,
                },
                new Pizza
                {
                    Name = "Prosciutto e funghi",
                    Description = "pomodoro, mozzarella, prosciutto cotto e funghi",
                    Foto = "/img/pizza06.jpg",
                    Price = 8,
                }
            };
            if (!Pizzas.Any())
            {
                Pizzas.AddRange(pizzaseed);
            }
            if (!Categories.Any())
            {
                var categorie = new Categoria[]
                {
                    new Categoria
                    {
                        Name = "Classica",
                        Pizzas = pizzaseed
                    },
                    new Categoria
                    {
                        Name = "Bianca"
                    },
                    new Categoria
                    {
                        Name = "Vegetariana"
                    },
                    new Categoria
                    {
                        Name = "Pizze di mare"
                    }

                };
                Categories.AddRange(categorie);
            }
            if (!Ingredients.Any())
            {
                var ingredientiseed = new Ingrediente[]
                {
                    new Ingrediente
                    {
                        Name = "Pomodoro",
                        Pizzas = pizzaseed
                    },
                    new Ingrediente
                    {
                        Name = "Mozzarella"
                    },
                    new Ingrediente
                    {
                        Name = "Sale"
                    },
                    new Ingrediente
                    {
                        Name = "Rucola"
                    },
                    new Ingrediente
                    {
                        Name = "Acciughe"
                    },
                    new Ingrediente
                    {
                        Name = "Tonno"
                    },
                    new Ingrediente
                    {
                        Name = "Salame"
                    }
                };
                Ingredients.AddRange(ingredientiseed);
                SaveChanges();
            }
        }
    }
}
