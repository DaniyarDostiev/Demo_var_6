using Demo_var_6.ApplicationData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
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
using Path = System.IO.Path;

namespace Demo_var_6.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductEditPage.xaml
    /// </summary>
    public partial class ProductEditPage : Page
    {
        Product product;
        string imagePath;
        string userName;
        int userRole;
        public ProductEditPage(Product product, string userName, int userRole)
        {
            InitializeComponent();

            this.product = product;
            this.userName = userName;
            this.userRole = userRole;

            productCategoryComboBoxTextBox.ItemsSource = PetStoreEntities.GetContext().Product.Select(x => x.ProductCategory).Distinct().ToList();

            checkProduct();
        }

        private void checkProduct()
        {
            string relativeImagePath;
            BitmapImage bitmapImage;
            if (product.ProductPhoto != null)
            {
                relativeImagePath = product.ProductPhoto;
                bitmapImage = new BitmapImage(new Uri(relativeImagePath, UriKind.Relative));
            } else
            {
                relativeImagePath = "..//ProductPictures/picture.png";
                bitmapImage = new BitmapImage(new Uri(relativeImagePath, UriKind.Relative));
            }
            
            
            productPicture.Source = bitmapImage;

            articleTextBox.Text = product.ProductArticleNumber;
            productNameTextBox.Text = product.ProductName;
            productCostTextBox.Text = product.ProductCost.ToString();
            productManufacturerTextBox.Text = product.ProductManufacturer;
            discountTextBox.Text = product.ProductDiscountAmount.ToString();
            quantityTextBox.Text = product.ProductQuantityInStock.ToString();
            descriptionTextBox.Text = product.ProductDescription;

            if (product.ProductCategory == null)
            {
                productCategoryComboBoxTextBox.SelectedIndex = 0;
            }
            else
            {
                productCategoryComboBoxTextBox.SelectedItem = product.ProductCategory;
            }

        }

        private void changePictureButtonClick(object sender, RoutedEventArgs e)
        {
            // Открываем диалоговое окно для выбора файла изображения
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Получаем выбранный путь к файлу
                    imagePath = openFileDialog.FileName;

                    string relativeImagePath = imagePath;
                    BitmapImage bitmapImage = new BitmapImage(new Uri(relativeImagePath));
                    productPicture.Source = bitmapImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }

        }
        private void exitButtonClick(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new AfterLoginPage(userName, userRole));
        }

        private void saveButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //product.ProductArticleNumber = articleTextBox.Text;
                product.ProductName = productNameTextBox.Text;
                product.ProductCost = int.Parse(productCostTextBox.Text);
                product.ProductManufacturer = productManufacturerTextBox.Text;
                product.ProductCategory = productCategoryComboBoxTextBox.SelectedItem.ToString();
                product.ProductDiscountAmount = int.Parse(discountTextBox.Text);
                product.ProductQuantityInStock = int.Parse(quantityTextBox.Text);
                product.ProductDescription = descriptionTextBox.Text;
                if (imagePath != null)
                {
                    product.ProductPhoto = imagePath;

                } else {
                    product.ProductPhoto = "..//ProductPictures/picture.png";
                }
                

                if (product.ProductArticleNumber == null)
                {
                    product.ProductArticleNumber = articleTextBox.Text;
                    PetStoreEntities.GetContext().Product.Add(product);
                }
                PetStoreEntities.GetContext().SaveChanges();

                MessageBox.Show("Данные сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

    }
}
