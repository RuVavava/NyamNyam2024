using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NyamNyam2024.DB;

namespace NyamNyam2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecipeDishPage.xaml
    /// </summary>
    public partial class RecipeDishPage : Page
    {
        int counter = 1;
        public static List<Dish> dishes {  get; set; }
        public static List<CookingStage> cookingStages { get; set; }
        public static List<IngredientOfStage> ingredientOfStages { get; set; }
        public static List<Ingredient> ingredients { get; set; }

        Dish context_dish;
        public RecipeDishPage(Dish dish)
        {
            InitializeComponent();
            context_dish = dish;

            nameDishesTB.Text = context_dish.Name;
            nameCategoryDishTB.Text = context_dish.Category.Name;
            shortdescrDishTB.Text = context_dish.Description;
            counter = context_dish.BaseServingsQuantity;

            var time = DBConnection.nnEntities.CookingStage.Where(x => x.DishId == context_dish.Id).Select(y => y.TimeInMinutes).ToList();
            int minuts = (int)time.Sum();
            int hours = minuts / 60;
            int newminuts = minuts - hours * 60;
            cookingTimeDishTB.Text = $"{hours} h. {newminuts} min. ({minuts} min.)";

            dishes = new List<Dish>(DBConnection.nnEntities.Dish.ToList());
            cookingStages = new List<CookingStage>(DBConnection.nnEntities.CookingStage.Where(x => x.DishId == context_dish.Id).ToList());
            ingredients = new List<Ingredient>(DBConnection.nnEntities.Ingredient.ToList());

            CookingStage.stepNumber = 1;

            ingredientOfStages = new List<IngredientOfStage>();
            var t = context_dish.CookingStage.SelectMany(s => s.IngredientOfStage).ToList();
            foreach (var i in t)
            {
                var w = ingredientOfStages.Find(x => x.IngredientId == i.IngredientId);
                if (w == null)
                {
                    i.SumQuantity = i.Quantity;
                    ingredientOfStages.Add(i);
                }
                else
                {
                    w.SumQuantity += i.Quantity;
                }
            }
            Refresh();

            this.DataContext = this;
        }

        private void Refresh()
        {
            totalCostDishsTB.Text = $"{context_dish.FinalPriceInDollar * counter} $";
            servingscountTB.Text = (counter).ToString();
            for (int i = 0; i < ingredientOfStages.Count; i++)
            {
                ingredientOfStages[i].TotalQuantity = ingredientOfStages[i].SumQuantity * counter;
                ingredientOfStages[i].TotalCost = ingredientOfStages[i].Ingredient.CostInCents * 0.01 * ingredientOfStages[i].TotalQuantity;
                ingredientOfStages[i].ColorRadioButtom = ingredientOfStages[i].Ingredient.AvailableCount >= ingredientOfStages.Where(x => x.IngredientId == ingredientOfStages[i].IngredientId).Sum(x => x.TotalQuantity);
            }
            RecipeDishesLv.ItemsSource = ingredientOfStages;
            RecipeDishesLv.Items.Refresh();
        }

        private void goBacktoDishesBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.DishesPage());
        }

        private void MinusBTN_Click(object sender, RoutedEventArgs e)
        {
            if (counter > 1)
                counter -= 1;
            Refresh();
        }

        private void PlusBTN_Click(object sender, RoutedEventArgs e)
        {
            counter += 1;
            Refresh();
        }
    }
}
