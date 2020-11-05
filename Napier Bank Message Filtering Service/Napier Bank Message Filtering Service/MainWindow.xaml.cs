﻿using System;
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
using BusinessLayer;

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
                    Email email = sf.ProcessEmail(txtSender.Text, txtHeader.Text, txtSubject.Text, txtBody.Text);
                    txtBody.Text = email.Text;

                    List<string> urls = email.QuarantinedURLs(msg);
                    
                    foreach (string s in urls)
                    {
                        lstURLs.Items.Add(s);
                    }
                }
                else if (txtHeader.Text.StartsWith("T"))
                {
                    string msg = txtBody.Text;
                    Tweet tweet = sf.ProcessTweet(txtSender.Text, txtHeader.Text, txtBody.Text);
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
    }
}
