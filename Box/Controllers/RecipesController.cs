using Box.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Box.Controllers
{
  public class RecipesController : Controller
  {
    private readonly BoxContext _db;
    private readonly UserManager<User> _userManager;
    public RecipesController(BoxContext db, UserManager<User> userManager)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Recipe> recipes = _db.Recipes.ToList();
      return View(recipes);
    }
    [Authorize]
    public ActionResult Create()
    {
      ViewBag.MealTypeId = new SelectList(_db.MealTypes, "MealTypeId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe, int MealTypeId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      if (MealTypeId != 0)
      {
        _db.MealTypeRecipes.Add(new MealTypeRecipe() { MealTypeId = MealTypeId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details (int id)
    {
      var thisRecipe = _db.Recipes
        .Include(recipe => recipe.MealTypes)
        .ThenInclude(join => join.MealType)
        // .Include(recipe => recipe.Ingredients)
        // .ThenInclude(join => join.Ingredient)
        .FirstOrDefault(recipe => recipe.RecipeId == id);
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userId != null)
      {
        ViewBag.IsCurrentUser = userId;
      }
      return View(thisRecipe);
    }

    public ActionResult AddIngredients(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "Name");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddIngredients(Recipe recipe, int IngredientId)
    {
      if (IngredientId != 0)
      {
        _db.RecipeIngredient.Add(new RecipeIngredient() {IngredientId = IngredientId, RecipeId = recipe.RecipeId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);

      Recipe thisRecipe = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).FirstOrDefault(recipes => recipes.RecipeId == id);
      if (thisRecipe == null)
      {
        return RedirectToAction("Details", new { id = id });
      }
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