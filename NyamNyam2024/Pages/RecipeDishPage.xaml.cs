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
        public static List<Dish> dishes {  get; set; }


        Dish context_dish;
        public RecipeDishPage(Dish dish)
        {
            InitializeComponent();
            context_dish = dish;

            nameDishesTB.Text = context_dish.Name;
            nameCategoryDishTB.Text = context_dish.Category.Name;
            shortdescrDishTB.Text = context_dish.Description;
            servingscountTB.Text = context_dish.BaseServingsQuantity.ToString();
            totalCostDishsTB.Text = $"{context_dish.FinalPriceInDollar * context_dish.BaseServingsQuantity} $";

            var time = DBConnection.nnEntities.CookingStage.Where(x => x.DishId == context_dish.Id).Select(y => y.TimeInMinutes).ToList();
            int minuts = (int)time.Sum();
            int hours = minuts / 60;
            int newminuts = minuts - hours * 60;
            cookingTimeDishTB.Text = $"{hours} h. {newminuts} min.";

            this.DataContext = this;
        }
    }
}
