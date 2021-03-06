using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Box.Models
{
public class BoxContext : IdentityDbContext<User>
{
  public DbSet<Recipe> Recipes { get; set; }
  public virtual DbSet<Ingredient> Ingredients { get; set; }
  public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
  public DbSet<MealType> MealTypes { get; set; }
  public DbSet<MealTypeRecipe> MealTypeRecipes { get; set; }
  public BoxContext(DbContextOptions options) : base(options) { }
  }
}