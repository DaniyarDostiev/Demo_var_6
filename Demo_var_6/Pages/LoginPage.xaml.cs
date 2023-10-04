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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void loginClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (loginTextbox.Text == "" || passwordTextbox.Text == "")
                {
                    MessageBox.Show("Заполните поля");
                }
                else
                {

                    var userObj = PetStoreEntities.GetContext().User.FirstOrDefault(x => x.UserLogin == loginTextbox.Text && x.UserPassword == passwordTextbox.Text);

                    if (userObj == null)
                    {
                        MessageBox.Show("Данные не верны, попробуйте ещё раз");
                    }
                    else
                    {
                        loginTextbox.Text = null;
                        passwordTextbox.Text = null;
                        AppFrame.mainFrame.Navigate(new AfterLoginPage(userObj.UserFullName, userObj.UserRole));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void guestClick(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new AfterLoginPage(null, 0));
        }
    }
}
