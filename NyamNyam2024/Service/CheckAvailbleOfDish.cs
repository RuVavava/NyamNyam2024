using NyamNyam2024.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamNyam2024.Service
{
    public class CheckAvailbleOfDish
    {
        public static void IsCheck()
        {
            List <Dish> dishes = DBConnection.nnEntities.Dish.ToList();

            for(int i = 0; i < dishes.Count; i++) 
            {
                dishes[i].Availble = true;
                int id_dish = dishes[i].Id;

                List<CookingStage> cookingStages = DBConnection.nnEntities.CookingStage.Where(x => x.DishId == id_dish).ToList();
                List<IngredientOfStage> ingredientOfStages = new List<IngredientOfStage>(0);

                for(int csc = 0; csc < cookingStages.Count; csc++)
                {
                    int stage_id = cookingStages[csc].Id;
                    List<IngredientOfStage> ingrofst = DBConnection.nnEntities.IngredientOfStage.Where(x => x.CookingStageId == stage_id).ToList();

                    for(int k=0; k < ingrofst.Count; k++)
                    {
                        ingredientOfStages.Add(ingrofst[k]);
                    }
                }

                for(int csc = 0; csc < ingredientOfStages.Count; csc++)
                {
                    if (ingredientOfStages[csc].Ingredient.AvailableCount < ingredientOfStages.Where(x => x.IngredientId == ingredientOfStages[csc].IngredientId).Sum(x => x.Quantity))
                    {
                        dishes[i].Availble = false;
                    }
                }
            }
            DBConnection.nnEntities.SaveChanges();
        }
    }
}
