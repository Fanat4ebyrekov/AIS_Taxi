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

namespace AIS_Taxi.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDepo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            DepoWindow depoWindow = new DepoWindow();
            depoWindow.Show();  
            this.Close();
        }

        private void btnTariff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnInfo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.Show();
            this.Close();
        }

        private void btDriver_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnWorker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
