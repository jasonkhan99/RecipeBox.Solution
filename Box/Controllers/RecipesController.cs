using Box.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Box.Controllers
{
  public class RecipesController : Controller
  {
    private readonly BoxContext _db;
    public RecipesController(BoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Recipe> model = _db.Recipes.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.MealTypeId = new SelectList(_db.MealTypes, "MealTypeId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Recipe recipe)
    {
      _db.Recipes.Add(recipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details (int id)
    {
      var thisRecipe = _db.Recipes
        .Include(recipe => recipe.MealTypes)
          .ThenInclude(join => join.MealType)
        .Include(recipe => recipe.Ingredients)
          .ThenInclude(join => join.Ingredient)
        .FirstOrDefault(recipe => recipe.RecipeId == id);
        return View(thisRecipe);
    }
    
    public ActionResult AddInst(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "Name");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddInst(Recipe recipe, int IngredientId)
    {
      if (IngredientId != 0)
      {
        _db.RecipeIngredient.Add(new RecipeIngredient() {IngredientId = IngredientId, RecipeId = recipe.RecipeId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete (int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }
    [HttpPost]
    public ActionResult Edit(Recipe recipe)
    {
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = recipe.RecipeId });
    }    
  }
}