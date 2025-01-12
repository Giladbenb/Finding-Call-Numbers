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

namespace Prog3B
{
    //St10090336 - Gilad Benbenishti - Part One
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Page Navigation
        private void RB_Click(object sender, RoutedEventArgs e)
        {
             ReplacingBooks objIncomePage = new ReplacingBooks();
             this.Visibility = Visibility.Hidden;
             objIncomePage.Show();
        }

        // Page Navigation
        private void IA_Click(object sender, RoutedEventArgs e)
        {
            Identifying_Areas objIncomePage = new Identifying_Areas();
            this.Visibility = Visibility.Hidden;
            objIncomePage.Show();
        }


        // Page Navigation
        private void FCN_Click(object sender, RoutedEventArgs e)
        {
             FindingCallNumbers objIncomePage = new FindingCallNumbers();
              this.Visibility = Visibility.Hidden;
             objIncomePage.Show();
        }
    }
}
