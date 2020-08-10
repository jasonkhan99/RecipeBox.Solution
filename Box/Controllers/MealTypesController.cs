using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Box.Models;

namespace Box.Controllers
{
  public class MealTypesController : Controller
  {
    private readonly BoxContext _db;
    public MealTypesController(BoxContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<MealType> model = _db.MealTypes.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(MealType mealType)
    {
      _db.MealTypes.Add(mealType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMealType = _db.MealTypes
      .Include(mealType => mealType.Recipes)
      .FirstOrDefault(mealType => mealType.MealTypeId == id);
      return View(thisMealType);
    }

    public ActionResult Edit(int id)
    {
      var thisMealType = _db.MealTypes.FirstOrDefault(mealType => mealType.MealTypeId == id);
      return View(thisMealType);
    }

    [HttpPost]
    public ActionResult Edit(MealType mealType)
    {
      _db.Entry(mealType).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = mealType.MealTypeId });
    }

    public ActionResult Delete(int id)
    {
      var thisMealType = _db.MealTypes.FirstOrDefault(mealType => mealType.MealTypeId == id);
      return View(thisMealType);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMealType = _db.MealTypes.FirstOrDefault(mealType => mealType.MealTypeId == id);
      _db.MealTypes.Remove(thisMealType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}