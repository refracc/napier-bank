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
            Random();
        }

        private void Random()
        {
            try
            {
                SMS sms = new SMS("1111111111", "jdjdjdjdjjdjdjdjdjddjdjdjdjdjjdjdjdjdjdd SSDD jdjdjdjdjjdwiuefhwiuefhwiuehfiuwefiuwehfiuwehf");
                MessageBox.Show(sms.Body, "EEEEEEERRRRRRRRROOOOOOORRRRRRRRRRRRRRRR", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.None);
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "EEEEEEERRRRRRRRROOOOOOORRRRRRRRRRRRRRRR", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.None);
            }
        }
    }
}
