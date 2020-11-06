using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
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

        private readonly ServiceFacade _sf = new ServiceFacade();

        /// <summary>
        /// This is what happens when you click the "Process" button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments</param>
        public void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtHeader.Text.StartsWith("S")) // Check its an SMS
                {
                    SMS sms = _sf.ProcessSMS(txtSender.Text, txtHeader.Text, txtBody.Text);
                    txtBody.Text = sms.Text; // Add content to text box

                }
                else if (txtHeader.Text.StartsWith("E")) // Check it's an email
                {
                    string msg = txtBody.Text; // Initialise msg text.
                    List<string> urls;

                    if (txtSubject.Text.StartsWith("SIR")) // Check if it is a SIR
                    {
                        SignificantIncidentReport sir = _sf.ProcessSIR(txtSender.Text, txtHeader.Text, txtSubject.Text, txtBody.Text);
                        txtBody.Text = sir.Text; 

                        urls = sir.QuarantinedURLs(msg);

                        lstSIR.Items.Add("Code: " + sir.Code);
                        lstSIR.Items.Add("Nature: " + sir.Nature);

                        foreach (string s in urls)
                        {
                            lstURLs.Items.Add(s); // Add quarantined URLs to list box
                        }
                        return;
                    }

                    // Do the same thing, but for a regular email.
                    
                    Email email = _sf.ProcessEmail(txtSender.Text, txtHeader.Text, txtSubject.Text, txtBody.Text);
                    txtBody.Text = email.Text;

                    urls = email.QuarantinedURLs(msg);

                    foreach (string s in urls)
                    {
                        lstURLs.Items.Add(s);
                    }
                }
                else if (txtHeader.Text.StartsWith("T")) // Check it's a tweet
                {
                    string msg = txtBody.Text;
                    Tweet tweet = _sf.ProcessTweet(txtSender.Text, txtHeader.Text, txtBody.Text);
                    txtBody.Text = tweet.Text;

                    List<string> mentions = tweet.ExtractMentions(msg);
                    List<string> hash = tweet.ExtractHashtags(msg);

                    foreach (string s in mentions)
                    {
                        lstMentions.Items.Add(s); // Put mentions in mention box
                    }

                    foreach (string s in hash)
                    {
                        lstHash.Items.Add(s); // Put hashtags in hash box
                    }

                }
                else
                {
                    MessageBox.Show(
                        "The header field starts with an invalid character. Please make this S, E or T followed by 9 characters.",
                        "Whoops!", MessageBoxButton.OK, MessageBoxImage.Exclamation); // Otherwise return error box
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// What happens when you click "Import" button.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments.</param>
        public void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog(); // Open GUI
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
                            txtBody.Text = line[3]; // Assume this data is correct

                            btnProcess_Click(sender, e); // Activate process button using the same arguments as this function.
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occurred...", MessageBoxButton.OK, MessageBoxImage.Error); // Otherwise return error message
            }
        }
    }
}
