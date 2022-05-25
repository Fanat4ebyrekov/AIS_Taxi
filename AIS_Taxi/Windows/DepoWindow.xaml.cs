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
    /// Логика взаимодействия для DepoWindow.xaml
    /// </summary>
    public partial class DepoWindow : Window
    {
        public DepoWindow()
        {
            InitializeComponent();
        }

        private void btnBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
