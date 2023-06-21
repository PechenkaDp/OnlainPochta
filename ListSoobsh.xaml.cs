using Email;
using System;
using System.Collections.Generic;
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

namespace OnlainPochta
{
    /// <summary>
    /// Логика взаимодействия для ListSoobsh.xaml
    /// </summary>
    public partial class ListSoobsh : Page
    {
        private ListPochty listPochty;
        private OknoPochty oknoPochty;

        private string toWhom;
        private string fromWhom;
        private string subject;
        private string message;
        public ListSoobsh(ListPochty list, OknoPochty user)
        {
            InitializeComponent();
            listPochty = list;
            oknoPochty = user;
            FromWhom.Text = globalVariables.addressPochty;
        }
        public void GetMessage(string To, string From, string Sub, string Message)
        {
            toWhom = To; fromWhom = From; subject = Sub; message = Message;
            ToWhom.Text = toWhom;
            FromWhom.Text = fromWhom;
            SubjectTbx.Text = subject;
            ConverterRTF.ToRtf(message);
            var text = new TextRange(MessageRtb.Document.ContentStart, MessageRtb.Document.ContentEnd);
            FileStream fs = new FileStream("msg.rtf", FileMode.Open);
            text.Load(fs, DataFormats.Rtf);
            fs.Close();

            File.Delete("msg.rtf");

        }

        private void AnswerBT_Click(object sender, RoutedEventArgs e)
        {
            SendMess send = new SendMess(listPochty, oknoPochty);
            send.ToTbx.Text = fromWhom;
            oknoPochty.FrameRec.Content = send;
        }

        private void ReturnBT_Click(object sender, RoutedEventArgs e)
        {
            oknoPochty.FrameRec.Content = listPochty;
        }
    }
}
