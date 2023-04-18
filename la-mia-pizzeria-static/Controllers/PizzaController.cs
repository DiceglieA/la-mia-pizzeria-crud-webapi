using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var ctx = new PizzeriaContext();

            var pizzas = ctx.Pizzas.ToArray();
            return View(pizzas);
        }

        //funzione create + validazioni

        [HttpGet]
        public IActionResult Create()
        {
            using var ctx = new PizzeriaContext();
            List<Categoria> categories = ctx.Categories.ToList();
            PizzaFormModel model = new PizzaFormModel();
            List<SelectListItem> listIngrediente = new List<SelectListItem>();

            foreach (Ingrediente ingrediente in ctx.Ingredients)
            {
                listIngrediente.Add(new SelectListItem()
                { Text = ingrediente.Name, Value = ingrediente.Id.ToString() });
            }
            model.Pizza = new Pizza();
            model.Categories = categories;
            model.Ingredients = listIngrediente;
            return View("Create", model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            var ctx = new PizzeriaContext();

            if (!ModelState.IsValid)
            {
                using (ctx = new PizzeriaContext())
                {
                    List<Ingrediente> ingredients = ctx.Ingredients.ToList();
                    List<SelectListItem> listIngrediente = new List<SelectListItem>();
                    foreach (Ingrediente ingrediente in ingredients)
                    {
                        listIngrediente.Add(
                            new SelectListItem()
                            { Text = ingrediente.Name, Value = ingrediente.Id.ToString() });
                    }
                    List<Categoria> categories = ctx.Categories.ToList();
                    data.Categories = categories;
                    data.Ingredients = listIngrediente;
                    return View("Create", data);
                }
            }
            using (ctx = new PizzeriaContext())
            {
                Pizza pizzaToCreate = new Pizza();
                pizzaToCreate.Name = data.Pizza.Name;
                pizzaToCreate.Description = data.Pizza.Description;
                pizzaToCreate.Foto = data.Pizza.Foto;
                pizzaToCreate.Price = data.Pizza.Price;
                pizzaToCreate.CategoriaId = data.Pizza.CategoriaId;
                if (data.SelectedIngredients != null)
                {
                    foreach (string selectedIngredienteId in data.SelectedIngredients)
                    {
                        int selectedIntIngredienteId = int.Parse(selectedIngredienteId);
                        Ingrediente? ingrediente = ctx.Ingredients
                                                   .Where(m => m.Id == selectedIntIngredienteId)
                                                   .FirstOrDefault();
                        pizzaToCreate.Ingredients.Add(ingrediente);

                    }
                }
                ctx.Pizzas.Add(pizzaToCreate);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //funzione edit + validazioni

        [HttpGet]
        public IActionResult Update(int id)
        {
            using PizzeriaContext ctx = new PizzeriaContext();
            Pizza? pizzaToUpdate = ctx.Pizzas.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                PizzaFormModel model = new PizzaFormModel();
                List<Categoria> categories = ctx.Categories.ToList();
                List<Ingrediente> ingredients = ctx.Ingredients.ToList();
                List<SelectListItem> listingrediente = new List<SelectListItem>();
                foreach (Ingrediente ingrediente in ingredients)
                {
                    listingrediente.Add(
                        new SelectListItem()
                        {
                            Text = ingrediente.Name,
                            Value = ingrediente.Id.ToString(),
                            Selected = pizzaToUpdate.Ingredients.Any(m => m.Id == ingrediente.Id)
                        });
                }
                model.Pizza = pizzaToUpdate;
                model.Categories = categories;
                model.Ingredients = listingrediente;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using PizzeriaContext context = new PizzeriaContext();
                List<Categoria> categories = context.Categories.ToList();
                List<Ingrediente> ingredients = context.Ingredients.ToList();
                List<SelectListItem> listIngrediente = new List<SelectListItem>();
                foreach (Ingrediente ingrediente in ingredients)
                {
                    listIngrediente.Add(
                        new SelectListItem()
                        { Text = ingrediente.Name, Value = ingrediente.Id.ToString() });
                }
                data.Ingredients = listIngrediente;
                data.Categories = categories;
                return View("Update", data);
            }
            using PizzeriaContext ctx = new PizzeriaContext();

            Pizza? pizzaEdit = ctx.Pizzas.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizzaEdit == null)
            {
                return NotFound();
            }
            pizzaEdit.Name = data.Pizza.Name;
            pizzaEdit.Description = data.Pizza.Description;
            pizzaEdit.Price = data.Pizza.Price;
            pizzaEdit.Foto = data.Pizza.Foto;
            pizzaEdit.CategoriaId = data.Pizza.CategoriaId;
            if (data.SelectedIngredients != null)
            {
                pizzaEdit.Ingredients.Clear();
                foreach (string selectedIngredienteId in data.SelectedIngredients)
                {
                    int selectedIntIngredienteId = int.Parse(selectedIngredienteId);
                    Ingrediente? ingrediente = ctx.Ingredients
                                              .Where(m => m.Id == selectedIntIngredienteId)
                                              .FirstOrDefault();
                    pizzaEdit.Ingredients.Add(ingrediente);
                }
            }

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        //funzione show
        public IActionResult Show(int id)
        {
            using var ctx = new PizzeriaContext();
            var pizza = ctx.Pizzas
                .Include(p => p.Categoria)
                .Include(p => p.Ingredients)
                .SingleOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        //funzione delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using var ctx = new PizzeriaContext();

            var pizzaDelete = ctx.Pizzas.FirstOrDefault(p => p.Id == id);
            if (pizzaDelete == null)
            {
                return NotFound();
            }

            ctx.Pizzas.Remove(pizzaDelete);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
