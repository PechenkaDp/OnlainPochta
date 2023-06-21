using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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

namespace OnlainPochta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string AddressPochty = "";
        string password;
        string NameService;
        string imyaPochty;
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void VhodButton_Click(object sender, RoutedEventArgs e)
        {
            password = Password.Text;
            imyaPochty = Pochta.Text;
            NameService = Service.Text;
            try
            {
                ImapHelper.Initialize((Service.SelectedItem as ComboBoxItem).Tag.ToString());

                if (ImapHelper.Login(AddressPochty, password))
                {


                    AddressPochty = Convert.ToString(imyaPochty + "@" + NameService);
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(AddressPochty);
                    globalVariables.PasswordPocht = password;
                    globalVariables.addressPochty = AddressPochty;
                    if (match.Success)
                    {
                        ImapHelper.Initialize(NameService.ToString());
                        if (ImapHelper.Login(AddressPochty, password))
                        {
                            OknoPochty okno = new OknoPochty();
                            okno.Show();
                             Close();
                        }
                        else MessageBox.Show("Неверный логин или пароль");

                    }
                }
                else
                {
                    MessageBox.Show("Login error");
                }
            }
            catch
            {
                MessageBox.Show("Вы ввели что-то неправильно");
            }

        }
    }
}
