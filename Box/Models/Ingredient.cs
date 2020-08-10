using System.Collections.Generic;

namespace Box.Models
{

  public class Ingredient
  {
    public int IngredientId { get; set; }

    public string Name { get; set; }

    public float Quantity { get; set; }

    public Ingerdient(int ingredientId, string name, float quantity)
    {
      IngredientId = ingredientId;
      Name = name;
      Quantity = Quantity;
    }
  }
}