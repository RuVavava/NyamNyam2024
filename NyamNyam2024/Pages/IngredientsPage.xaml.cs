using NyamNyam2024.DB;
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


namespace NyamNyam2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для IngredientsPage.xaml
    /// </summary>
    public partial class IngredientsPage : Page
    {
        public static List<Ingredient> ingridients { get; set; }
        public IngredientsPage()
        {
            InitializeComponent();
            ingridients = new List<Ingredient>(DBConnection.nnEntities.Ingredient.ToList());

            Refresh();

            this.DataContext = this;
        }

        private void PlusBTN_Click(object sender, RoutedEventArgs e)
        {
            var ingridient = (sender as Button).DataContext as Ingredient;

            if (ingridient.AvailableCount == 99)
                return;
            ingridient.AvailableCount += 1;
            DBConnection.nnEntities.SaveChanges();
            Refresh();
        }

        private void MinusBTN_Click(object sender, RoutedEventArgs e)
        {
            var ingridient = (sender as Button).DataContext as Ingredient;

            if (ingridient.AvailableCount == 1)
                return;
            ingridient.AvailableCount -= 1;
            DBConnection.nnEntities.SaveChanges();
            Refresh();
        }

        private void Refresh()
        {
            var s = DB.DBConnection.nnEntities.TotalSummIngridientsPr();
            int totalSummIngridients = (int)s.Sum();
            totalcostTBL.Text = $"{totalSummIngridients * 0.01} $";

            ingridientsLV.ItemsSource = new List<Ingredient>(DBConnection.nnEntities.Ingredient.ToList());
        }

        private void countIngredientTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            DBConnection.nnEntities.SaveChanges();
        }

        private void DeleteIndredientHL_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Hyperlink hyperlink && hyperlink.DataContext is Ingredient ingredient)
            {
                var findIngredienWhichInDishes = DBConnection.nnEntities.FindIngredienWhichInDishes();
                List<FindIngredienWhichInDishes_Result> cantRemoveIngridient = findIngredienWhichInDishes.ToList();
                

                var ingr = cantRemoveIngridient.Find(x => x.Id == ingredient.Id);

                if (ingr == null)
                {                    
                    DBConnection.nnEntities.Ingredient.Remove(ingredient);
                    DBConnection.nnEntities.SaveChanges();
                    MessageBox.Show($"The removal was successful!");

                    Refresh();
                }
                else
                    MessageBox.Show($"Removal is not possible. {ingr.Name} uise in {ingr.Count} dishes") ;
            }
        }
    }
}
