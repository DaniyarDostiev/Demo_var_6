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
        public AfterLoginPage(string userName, int userRole)
        {
            InitializeComponent();
            userNameTextBox.Text = userName;
            if (userRole == 1)
            {
                userRoleTextBox.Text = "Админ";
            } 
            else if (userRole == 2)
            {
                userRoleTextBox.Text = "Клиент";
            }
            else
            {
                userRoleTextBox.Text = "Менеджер";
            }
            
        }
    }
}
