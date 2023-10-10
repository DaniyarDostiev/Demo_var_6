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

using Demo_var_6.ApplicationData;

namespace Demo_var_6.Pages
{
    /// <summary>
    /// Логика взаимодействия для AfterLoginPage.xaml
    /// </summary>
    public partial class AfterLoginPage : Page
    {
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
            
        }

        private void exitButtonClick(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new LoginPage());
        }
    }
}
