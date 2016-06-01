using FirstFloor.ModernUI.Presentation;
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
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : UserControl
    {
        public AccountPage()
        {
            InitializeComponent();
            Update();
            this.Loaded += AccountPage_Loaded;
        }

        private void AccountPage_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public void StyleUpdate(Uri ThemeSource, Color color, int fontSize, bool forceCursor)
        {
            AppearanceManager.Current.ThemeSource = ThemeSource;
            AppearanceManager.Current.AccentColor = color;
            llogin.FontSize = fontSize;
            llogin.ForceCursor = forceCursor;
            lfirstname.FontSize = fontSize;
            lfirstname.ForceCursor = forceCursor;
            llastname.FontSize = fontSize;
            llastname.ForceCursor = forceCursor;
            lemail.FontSize = fontSize;
            lemail.ForceCursor = forceCursor;
        }

        public void Update()
        {
            llogin.Content = "";
            lfirstname.Content = "";
            llastname.Content = "";
            lemail.Content = "";
            var dbRequest = new DbRequest();
            var user = dbRequest.LogIn(LogIn.login, LogIn.password);
            if (user == null)
            {
                llogin.Content = "Пользователь не найден";
            }
            else
            {
                llogin.Content = "Login: " + user.login;
                lfirstname.Content = "First name: " + user.first_name;
                llastname.Content = "Last name: " + user.last_name;
                lemail.Content = "Email: " + user.email;
                switch (user.role_id)
                {
                    case 1:
                        StyleUpdate(AppearanceManager.DarkThemeSource, Color.FromRgb(0x60, 0xa9, 0x17), 25, false);
                        break;
                    case 2:
                        StyleUpdate(AppearanceManager.DarkThemeSource, Color.FromRgb(0x00, 0xab, 0xa9), 18, false);
                        break;
                    case 3:
                        StyleUpdate(AppearanceManager.LightThemeSource, Color.FromRgb(0xaa, 0x00, 0xff), 18, false);
                        break;
                    case 4:
                        StyleUpdate(AppearanceManager.LightThemeSource, Color.FromRgb(0x00, 0x50, 0xef), 14, true);
                        break;
                    default:
                        StyleUpdate(AppearanceManager.DarkThemeSource, Color.FromRgb(0xa4, 0xc4, 0x00), 10, true);
                        break;
                }
            }
        } 
    }
}
