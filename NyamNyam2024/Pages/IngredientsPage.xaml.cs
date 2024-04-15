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
            NavigationService.Navigate(new IngredientsPage());
        }

        private void MinusBTN_Click(object sender, RoutedEventArgs e)
        {
            var ingridient = (sender as Button).DataContext as Ingredient;

            if (ingridient.AvailableCount == 1)
                return;
            ingridient.AvailableCount -= 1;
            DBConnection.nnEntities.SaveChanges();
            Refresh();
            NavigationService.Navigate(new IngredientsPage());
        }

        private void Refresh()
        {
            ingridientsLV.ItemsSource = new List<Ingredient>(DBConnection.nnEntities.Ingredient.ToList());
        }
    }
}
