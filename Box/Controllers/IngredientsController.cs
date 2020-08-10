using Box.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Box.Controllers
{
  public class IngredientsController : Controller
  {
    private readonly BoxContext _db;
    
    public IngredientsController(BoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Ingredients.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Ingredient ingredient, int RecipeId)
    {
      _db.Ingredients.Add(ingredient);
      if (RecipeId != 0)
      {
        _db.RecipeIngredient.Add(new RecipeIngredient() { RecipeId = RecipeId, IngredientId = ingredient.IngredientId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisIngredient = _db.Ingredients
          .Include(ingredient => ingredient.Recipes)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }

    public ActionResult Delete(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredients => ingredients.IngredientId == id);
      return View(thisIngredient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredients => ingredients.IngredientId == id);
      _db.Ingredients.Remove(thisIngredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}