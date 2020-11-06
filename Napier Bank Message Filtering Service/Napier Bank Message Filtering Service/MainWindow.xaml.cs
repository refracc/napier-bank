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
using BusinessLayer;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Napier_Bank_Message_Filtering_Service
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly ServiceFacade sf = new ServiceFacade();

        public void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtHeader.Text.StartsWith("S"))
                {
                    SMS sms = sf.ProcessSMS(txtSender.Text, txtHeader.Text, txtBody.Text);
                    txtBody.Text = sms.Text;

                }
                else if (txtHeader.Text.StartsWith("E"))
                {
                    string msg = txtBody.Text;
                    List<string> urls;

                    if (txtSubject.Text.StartsWith("SIR"))
                    {
                        SignificantIncidentReport sir = sf.ProcessSIR(txtSender.Text, txtHeader.Text, txtSubject.Text, txtBody.Text);
                        txtBody.Text = sir.Text;

                        urls = sir.QuarantinedURLs(msg);

                        lstSIR.Items.Add("Code: " + sir.Code);
                        lstSIR.Items.Add("Nature: " + sir.Nature);

                        foreach (string s in urls)
                        {
                            lstURLs.Items.Add(s);
                        }
                        return;
                    }
                    
                    Email email = sf.ProcessEmail(txtSender.Text, txtHeader.Text, txtSubject.Text, txtBody.Text);
                    txtBody.Text = email.Text;

                    urls = email.QuarantinedURLs(msg);

                    foreach (string s in urls)
                    {
                        lstURLs.Items.Add(s);
                    }
                }
                else if (txtHeader.Text.StartsWith("T"))
                {
                    string msg = txtBody.Text;
                    Tweet tweet = sf.ProcessTweet(txtSender.Text, txtHeader.Text, txtBody.Text);
                    txtBody.Text = tweet.Text;

                    List<string> mentions = tweet.ExtractMentions(msg);
                    List<string> hash = tweet.ExtractHashtags(msg);

                    foreach (string s in mentions)
                    {
                        lstMentions.Items.Add(s);
                    }

                    foreach (string s in hash)
                    {
                        lstHash.Items.Add(s);
                    }

                }
                else
                {
                    MessageBox.Show(
                        "The header field starts with an invalid character. Please make this S, E or T followed by 9 characters.",
                        "Whoops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    string extension = Path.GetExtension(ofd.FileName);

                    if (extension.Equals(".csv"))
                    {
                        string[] data = File.ReadAllLines(ofd.FileName);

                        for (int i = 1; i < data.Length; i++)
                        {
                            string[] line = data[i].Split(",");

                            txtHeader.Text = line[0];
                            txtSender.Text = line[1];
                            txtSubject.Text = line[2];
                            txtBody.Text = line[3];

                            btnProcess_Click(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occurred...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
