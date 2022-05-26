using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using AIS_Taxi.EF;

namespace AIS_Taxi.Windows
{
    /// <summary>
    /// Логика взаимодействия для DepoWindow.xaml
    /// </summary>
    public partial class DepoWindow : Window, INotifyPropertyChanged
    {
        private List<EF.Car> listCar;

        public List<EF.Car> ListCar
        {
            get { return listCar; }
            set
            {
                listCar = value;

                OnPropertyChanged();


            }
        }

        private object selectedItemGrid;

        public object SelectedItemGrid
        {

            get { return selectedItemGrid; }
            set
            {
                selectedItemGrid = value;
                OnPropertyChanged();

                Car car = (Car)value;
                tbNumber.Text = car.NumberCar;
                tbDateMade.DataContext = car.DateMade;
                tbBrand.Text = car.Brand;
                tbCarrying.Text = car.Carrying;
                //cbDriver. = car.Driver;
            }
        }

        public DepoWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ListCar = Clasess.Entities.context.Car.ToList();
             
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
