using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        public IActionResult GetPizzas([FromQuery] string? title)
        {
            using var ctx = new PizzeriaContext();
            var pizzas = ctx.Pizzas.Where(p => title == null || p.Name.ToLower().Contains(title.ToLower()))
                .ToList();

            return Ok(pizzas);

        }

        [HttpGet("{id}")]
        public IActionResult GetPizzas(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizza = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();

            }
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult CreatePizza(Pizza pizza)
        {
            using var ctx = new PizzeriaContext();
            ctx.Pizzas.Add(pizza);
            ctx.SaveChanges();
            return Ok(pizza);

        }

        [HttpPut("{id}")]
        public IActionResult PutPizza(int id, [FromBody] Pizza pizza)
        {
            using var ctx = new PizzeriaContext();
            var savedPizza = ctx.Pizzas.FirstOrDefault(pizza => pizza.Id == id);
            if (savedPizza == null)
            {
                return NotFound();
            }
            savedPizza.Name = pizza.Name;
            savedPizza.Description = pizza.Description;
            savedPizza.Foto = pizza.Foto;
            savedPizza.Price = pizza.Price;
            savedPizza.CategoriaId = pizza.CategoriaId;

            ctx.SaveChanges();

            return Ok(savedPizza);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePizza(int id)
        {
            using var ctx = new PizzeriaContext();
            var savedPizza = ctx.Pizzas?.FirstOrDefault(p => p.Id == id);

            if (savedPizza == null)
            {
                return NotFound();
            }
            ctx.Pizzas.Remove(savedPizza);
            ctx.SaveChanges();
            return Ok(savedPizza);
        }

       
    }
}
