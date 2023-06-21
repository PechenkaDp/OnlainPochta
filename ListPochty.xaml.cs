using ImapX;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlainPochta
{
    /// <summary>
    /// Логика взаимодействия для ListPochty.xaml
    /// </summary>
    public partial class ListPochty : Page
    {
        private OknoPochty oknoPochty;
        MessageCollection messages;
        List<string> messagesList = new List<string>();
        public ListPochty(OknoPochty window)
        {
            InitializeComponent();
            oknoPochty = window;
        }



        public void Folder(string folder)
        {
            messages = ImapHelper.GetMessagesForFolder(folder);
            if (messages != null)
            {
                Messages.ItemsSource = null;
                foreach (Message m in messages)
                {
                    messagesList.Add(m.Subject);
                }
                Messages.ItemsSource = messagesList;
            }
            oknoPochty.Zagruzka.Visibility = Visibility.Hidden;

        }

        private void DoubleClickListBox(object sender, MouseButtonEventArgs e)
        {
            if (Messages.SelectedItem != null)
            {
                OpenningLetter();
            }
        }

        private void MessagesLbx_Click(object sender, RoutedEventArgs e)
        {
            if (sender == Open)
            {
                OpenningLetter();
            }
            else
            {
                SendMess page = new SendMess(this, oknoPochty);
                page.GetAddress(messages[Messages.SelectedIndex].From.Address);
                oknoPochty.FrameRec.Content = page;
            }
        }
        private void OpenningLetter()
        {
            var text = messages[Messages.SelectedIndex].Body.Html;
            string to = "";
            foreach (var i in messages[Messages.SelectedIndex].To)
            {
                to = i.Address;
                break;
            }
            string from = messages[Messages.SelectedIndex].From.Address.ToString();
            string subject = messages[Messages.SelectedIndex].Subject;
            ListSoobsh readLetterPage = new ListSoobsh(this, oknoPochty);
            readLetterPage.GetMessage(to, from, subject, text);
            oknoPochty.FrameRec.Content = readLetterPage;
        }
    }
}
