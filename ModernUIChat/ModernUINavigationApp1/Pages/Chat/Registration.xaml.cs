using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Models;
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

namespace ModernUINavigationApp1.Pages.Chat
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : UserControl
    {

        private DbRequest dbrequest;

        public Registration()
        {
            InitializeComponent();
            dbrequest = new DbRequest();

            var roles = dbrequest.GetRoles();
            roleComboBox.ItemsSource = roles;
            roleComboBox.SelectedIndex = 0;

            this.Loaded += Registration_Loaded;
        }

        private void Registration_Loaded(object sender, RoutedEventArgs e)
        {
            IsCreate.Content = "";
        }

        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new User()
            {
                login = loginTextBox.Text,
                email = emailTextBox.Text,
                first_name = firsNameTextBox.Text,
                last_name = lastNameTextBox.Text,
                password = passwordBox.Password,
                role_id = ((Role)roleComboBox.SelectedItem).id
            };
            var response = dbrequest.CreateNewAccount(user);
            if (response == null)
            {
                IsCreate.Content = "Error";
            }
            else
            {
                IsCreate.Content = "Account created!";
            }
        }
    }
}
