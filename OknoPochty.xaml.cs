using ImapX.Collections;
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
using System.Windows.Shapes;

namespace OnlainPochta
{
    /// <summary>
    /// Логика взаимодействия для OknoPochty.xaml
    /// </summary>
    public partial class OknoPochty : Window
    {
        private CommonFolderCollection folders = ImapHelper.GetFolders();
        public OknoPochty()
        {

            InitializeComponent();
            AdressPochty1.Text = globalVariables.addressPochty;
            foreach (var item in folders)
            {
                Mails.Items.Add(item.Name);
            }
        }

        private void Mails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Mails.SelectedItems != null)
            {
                ListPochty soobsh = new ListPochty(this);
                string item = Mails.SelectedItem.ToString();
                soobsh.Folder(item);
                FrameRec.Content = soobsh;
            }
        }

        private void Otprav_Click(object sender, RoutedEventArgs e)
        {
                SendMess readLetter = new SendMess(null, null);
                FrameRec.Content = readLetter;
        }
    }
}
