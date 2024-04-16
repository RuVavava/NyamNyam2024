using NyamNyam2024.DB;
using NyamNyam2024.Service;
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
    /// Логика взаимодействия для DishesPage.xaml
    /// </summary>
    public partial class DishesPage : Page
    {
        public static List<Dish> dishes { get; set; }
        public static List<Category> categories { get; set; }
        public DishesPage()
        {
            InitializeComponent();
            CheckAvailbleOfDish.IsCheck();

            dishes = new List<Dish>(DBConnection.nnEntities.Dish.ToList());
            categories = new List<Category>(DBConnection.nnEntities.Category.ToList());
            categories.Insert(0, new Category() { Name = "All catigories"});
            categoriesCB.SelectedIndex = 0;
            this.DataContext = this;
        }

        private void SearchNameDishesTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchNameDishesTB.Text.Length > 0)
            {
                dishesLV.ItemsSource = DBConnection.nnEntities.Dish.Where(i => i.Name.ToLower().StartsWith(SearchNameDishesTB.Text.Trim().ToLower())
                    || i.Description.ToLower().StartsWith(SearchNameDishesTB.Text.Trim().ToLower())).ToList();
            }
            else
            {
                dishesLV.ItemsSource = new List<Dish>(DBConnection.nnEntities.Dish.ToList());
            }
        }

        private void categoriesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var name = categoriesCB.SelectedItem as Category;
            if (categoriesCB.SelectedIndex >= 0 && name != null)
            {
                dishesLV.ItemsSource = DBConnection.nnEntities.Dish.Where(x => x.CategoryId == name.Id).ToList();
                if (categoriesCB.SelectedIndex == 0)
                {
                    dishesLV.ItemsSource = dishes;
                }

            }
        }

        private void IngredientsChB_Unchecked(object sender, RoutedEventArgs e)
        {


            if (IngredientsChB.IsChecked == false)
            {
                dishesLV.ItemsSource = DBConnection.nnEntities.Dish.ToList();
            }
            else
            {
                dishesLV.ItemsSource = dishes;
            }
        }

        private void IngredientsChB_Checked(object sender, RoutedEventArgs e)
        {
            if (IngredientsChB.IsChecked == true)
            {
                dishesLV.ItemsSource = DBConnection.nnEntities.Dish.Where(x => x.Availble == true).ToList();
            }
            else
            {
                dishesLV.ItemsSource = dishes;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            List<Dish> dishes = new List<Dish>(DBConnection.nnEntities.Dish.ToList());
            dishes = dishes.Where(x => x.FinalPriceInDollar <= priceSlider.Value).ToList();
            priceslvalue.Text = $"{Math.Round(priceSlider.Value, 2)} $";
            dishesLV.ItemsSource = dishes;

        }

        private void SearchByMultipleParameters()
        {
            List<Dish> dishes = new List<Dish>(DBConnection.nnEntities.Dish.ToList());

            //ПОИСК ПО НАИМЕНОВАНИЮ И ОПИСАНИЮ
            if (SearchNameDishesTB.Text.Length > 0)
            {
                dishesLV.ItemsSource = DBConnection.nnEntities.Dish.Where(i => i.Name.ToLower().StartsWith(SearchNameDishesTB.Text.Trim().ToLower())
                    || i.Description.ToLower().StartsWith(SearchNameDishesTB.Text.Trim().ToLower())).ToList();
            }

            //ПОИСК ПО КАТЕГОРИИ
            var name = categoriesCB.SelectedItem as Category;
            if (categoriesCB.SelectedIndex >= 0 && name != null)
            {
                dishesLV.ItemsSource = DBConnection.nnEntities.Dish.Where(x => x.CategoryId == name.Id).ToList();
                if (categoriesCB.SelectedIndex == 0)
                {
                    dishesLV.ItemsSource = dishes;
                }

            }

            //ЧЕКБОКС

            if (IngredientsChB.IsChecked == true)
            {
                dishesLV.ItemsSource = DBConnection.nnEntities.Dish.Where(x => x.Availble == true).ToList();
            }
            else
            {
                dishesLV.ItemsSource = dishes;
            }

            //СЛАЙДЕР
            dishes = dishes.Where(x => x.FinalPriceInDollar <= priceSlider.Value).ToList();
            priceslvalue.Text = $"{Math.Round(priceSlider.Value, 2)} $";
            dishesLV.ItemsSource = dishes;
        }

        private void dishesLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dishesLV.SelectedItem is Dish dish)
            {
                dishesLV.SelectedItem = null;
                NavigationService.Navigate(new RecipeDishPage(dish));
            }
        }
    }
}
