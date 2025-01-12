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
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Prog3B
{
    //St10090336 - Gilad Benbenishti - Part One
    /// <summary>
    /// Interaction logic for ReplacingBooks.xaml
    /// </summary>
    public partial class ReplacingBooks : Window
    {
        private ObservableCollection<ReplaceBook> items;

        // public string CallNumber { get; private set; }

        public ReplacingBooks()
        {
            InitializeComponent();

            items = new ObservableCollection<ReplaceBook>(GenerateRandomCallNumbers(10));
            callNumberListView.ItemsSource = items;
        }
        // Generator for random numbers 
        private List<ReplaceBook> GenerateRandomCallNumbers(int count)
        {
            Random random = new Random();
            List<ReplaceBook> Numbers = new List<ReplaceBook>();
            for (int i = 0; i < count; i++)
            {
                string number = $"{random.Next(1000, 10000):0000}.{(char)random.Next('A', 'Z' + 1)}{(char)random.Next('A', 'Z' + 1)}";
                Numbers.Add(new ReplaceBook { CallNumber = number });
            }
            return Numbers;
        }

        // Mouse move function
        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is FrameworkElement frameworkElement)
            {
                DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, frameworkElement.DataContext), DragDropEffects.Move);
            }
        }

        // Call Number List View
        private void callNumberListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                var listView = sender as ListView;
                var position = e.GetPosition(listView);
                var targetItem = GetListViewItem(listView, position);

                if (targetItem != null)
                {
                    ReplaceBook draggedItem = e.Data.GetData(DataFormats.Serializable) as ReplaceBook;
                    ReplaceBook target = targetItem.DataContext as ReplaceBook;

                    int targetIndex = items.IndexOf(target);
                    int draggedIndex = items.IndexOf(draggedItem);

                    if (targetIndex != -1 && draggedIndex != -1)
                    {
                        items.Move(draggedIndex, targetIndex);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect try again :(" + draggedIndex + " " + targetIndex);
                    }
                }
            }
        }

        //ListView Item
        private ListViewItem GetListViewItem(ListView listView, Point position)
        {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(listView, position);
            DependencyObject target = hitTestResult.VisualHit;

            while (target != null && !(target is ListViewItem))
            {
                target = VisualTreeHelper.GetParent(target);
            }

            return target as ListViewItem;
        }


        class ReplaceBook
        {
            public String CallNumber { get; set; }
        }

        //Button clicked action
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isAscending = IsListInAscendingOrder(items);

            if (isAscending)
            {
                MessageBox.Show("Congratulations,  The list is in ascending order.");
            }
            else
            {
                MessageBox.Show("Error, The list is NOT in ascending order.");
            }

        }

        // Ascending order 
        private bool IsListInAscendingOrder(ObservableCollection<ReplacingBooks> items)
        {
            throw new NotImplementedException();
        }

        private bool IsListInAscendingOrder(ObservableCollection<ReplaceBook> booksList)
        {
            var sortedList = booksList.OrderBy(book => book.CallNumber).ToList();

            return booksList.SequenceEqual(sortedList);
        }

        //Page Navigation
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow objIncomePage = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objIncomePage.Show();
        }
    }
}

        






