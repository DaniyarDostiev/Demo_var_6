using Demo_var_6.ApplicationData;
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

namespace Demo_var_6.Pages
{
    /// <summary>
    /// Логика взаимодействия для AfterLoginPage.xaml
    /// </summary>
    public partial class AfterLoginPage : Page
    {
        PageSwitcher pageSwitcher;
        public AfterLoginPage(string userName, int userRole)
        {
            InitializeComponent();

            userNameLabel.Content = userName;
            if (userRole == 1)
            {
                userRoleLabel.Content = "Админ";
            } 
            else if (userRole == 2)
            {
                userRoleLabel.Content = "Клиент";
            }
            else if (userRole == 3)
            {
                userRoleLabel.Content = "Менеджер";
            }
            else
            {
                userRoleLabel.Content = "Гость";
            }

            comboBoxFilter.Items.Add("Фильтры");
            foreach (var productManufacturer in PetStoreEntities.GetContext().Product.Select(x => x.ProductManufacturer).Distinct())
            {
                comboBoxFilter.Items.Add(productManufacturer);
            }
            comboBoxFilter.SelectedIndex = 0;

            comboBoxSort.Items.Add("Сортировка");
            comboBoxSort.Items.Add("По возрастанию");
            comboBoxSort.Items.Add("По убыванию");
            comboBoxSort.SelectedIndex = 0;
        }

        private void findProducts()
        {
            List<Product> products = PetStoreEntities.GetContext().Product
                .Where(x => x.ProductName.Contains(textBoxFinder.Text) || 
                x.ProductDescription.Contains(textBoxFinder.Text) || 
                x.ProductManufacturer.Contains(textBoxFinder.Text)).ToList();

            switch (comboBoxSort.SelectedIndex)
            {
                case 0:; break;
                case 1: products = products.OrderBy(x => x.ProductCost).ToList(); break;
                case 2: products = products.OrderByDescending(x => x.ProductCost).ToList(); break;
            }

            if (comboBoxFilter.SelectedIndex > 0)
            {
                string productManufacturer = comboBoxFilter.SelectedItem.ToString();
                products = products.Where(x => x.ProductManufacturer == productManufacturer).ToList();
            }

            listBoxProducts.ItemsSource = products;

            pageSwitcher = new PageSwitcher(listBoxProducts, products);
            stackPanelPageSwitcher.Children.Clear();
            stackPanelPageSwitcher.Children.Add(pageSwitcher.gPageSwitcher);

        }

        private void exitButtonClick(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new LoginPage());
        }

        private void editButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void addButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void textBoxFinderChanged(object sender, TextChangedEventArgs e)
        {
            findProducts();
        }

        private void comboBoxSortChanged(object sender, SelectionChangedEventArgs e)
        {
            findProducts();
        }

        private void comboBoxFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            findProducts();
        }


        public class PageSwitcher
        {
            private int currentPage = 1;
            private int countPages = 1;
            private const int countElementsOnPage = 5;
            private const int countPagesOnSwitcher = 5;

            private ListBox listBox = new ListBox();
            private List<Product> products = new List<Product>();

            private StackPanel stackPanelPageSwitcher = new StackPanel();
            public Grid gPageSwitcher = new Grid();

            public PageSwitcher(ListBox listBox, List<Product> products)
            {
                this.products = products;
                this.listBox = listBox;

                ConsiderCountPage();
                CreateSpPageSwitcher();
                CreateGridPageSwitcher();
                CreatePages();
                ShowProductsOnPage();
            }

            private void ConsiderCountPage()
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (i % countElementsOnPage == 0 && i != 0)
                    {
                        countPages++;
                    }
                }
            }

            private void CreateSpPageSwitcher()
            {
                stackPanelPageSwitcher.Orientation = Orientation.Horizontal;
                stackPanelPageSwitcher.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(stackPanelPageSwitcher, 1);
            }

            private void CreateGridPageSwitcher()
            {
                gPageSwitcher.ColumnDefinitions.Add(new ColumnDefinition());
                gPageSwitcher.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
                gPageSwitcher.ColumnDefinitions.Add(new ColumnDefinition());
            }

            private void CreatePage(int i)
            {
                Label lbPage = new Label();
                lbPage.Content = i;
                lbPage.MouseLeftButtonDown += LbPage_MouseLeftButtonDown;

                if (i == currentPage)
                {
                    lbPage.Foreground = Brushes.Red;
                }
                stackPanelPageSwitcher.Children.Add(lbPage);
            }

            private void CreatePages()
            {
                gPageSwitcher.Children.Clear();
                stackPanelPageSwitcher.Children.Clear();
                Label lbBack = new Label();
                lbBack.Content = "<";
                lbBack.MouseLeftButtonDown += LbBack_MouseLeftButtonDown;
                Grid.SetColumn(lbBack, 0);
                gPageSwitcher.Children.Add(lbBack);

                Label lbNext = new Label();
                lbNext.Content = ">";
                lbNext.MouseLeftButtonDown += LbNext_MouseLeftButtonDown;
                Grid.SetColumn(lbNext, 2);
                gPageSwitcher.Children.Add(lbNext);

                if (currentPage < countPagesOnSwitcher)
                {
                    for (int i = 1; i <= countPagesOnSwitcher; i++)
                    {
                        if (i > countPages)
                        {
                            break;
                        }
                        CreatePage(i);
                    }
                }
                else if (currentPage <= countPages - countPagesOnSwitcher)
                {
                    for (int i = currentPage; i < currentPage + countPagesOnSwitcher; i++)
                    {
                        CreatePage(i);
                    }
                }
                else if (currentPage > countPages - countPagesOnSwitcher)
                {
                    for (int i = countPages - countPagesOnSwitcher + 1; i <= countPages; i++)
                    {
                        CreatePage(i);
                    }
                }

                gPageSwitcher.Children.Add(stackPanelPageSwitcher);
            }

            private void ShowProductsOnPage()
            {
                List<Product> newAgents = new List<Product>();
                for (int i = currentPage * countElementsOnPage - countElementsOnPage;
                    i < currentPage * countElementsOnPage; i++)
                {
                    if (i >= products.Count())
                    {
                        break;
                    }
                    newAgents.Add(products[i]);
                }
                listBox.ItemsSource = newAgents;
            }

            private void LbPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                Label label = (Label)sender;
                currentPage = int.Parse(label.Content.ToString());
                ShowProductsOnPage();
                CreatePages();
            }

            private void LbNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (currentPage < countPages)
                {
                    currentPage++;
                }
                ShowProductsOnPage();
                CreatePages();
            }

            private void LbBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                }
                ShowProductsOnPage();
                CreatePages();
            }
        }
    }
}
