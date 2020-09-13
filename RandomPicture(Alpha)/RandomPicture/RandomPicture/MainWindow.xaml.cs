using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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


namespace RandomPicture
{

    public partial class MainWindow : Window
    {
        Thread refresh;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            RefreshPicture.AddPicture(ListPicture);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if(ListPicture.SelectedItems != null)
            {
                ListPicture.Items.Remove(ListPicture.SelectedItem);
            }
        }

        private void hour_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label_hour.Content = "Количество часов " + Convert.ToInt32(hour.Value);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            status.Items.Add("Выполняется");
            this.WindowState = WindowState.Minimized;
            RefreshPicture.RefreshPicturer(ListPicture, Convert.ToInt32(hour.Value), Convert.ToInt32(min.Value));
            this.WindowState = WindowState.Normal;
            status.Items.Add(@"Завершило выполнение и ожидает указаний");
        }

        private void min_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label_min.Content = "Количество минут " + Convert.ToInt32(min.Value);
        }

        private void ListPicture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListPicture.SelectedItem != null) { 
            image.Source = new BitmapImage(new Uri( ListPicture.SelectedItem.ToString()));
        
            }
        }
        public void P()
        {
            Thread thread = new Thread(() => { Dispatcher.Invoke((Action)(() => { RefreshPicture.RefreshPicturer(ListPicture, Convert.ToInt32(hour.Value), Convert.ToInt32(min.Value)); })); });
            refresh = thread;
            refresh.Start();
        }
    }
}
