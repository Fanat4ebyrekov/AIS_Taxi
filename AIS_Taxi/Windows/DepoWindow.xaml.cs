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
using static AIS_Taxi.Clasess.Entities;
using AIS_Taxi.Clasess;

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
                tbDateMade.Text = Convert.ToString(car.DateMade.Date);
                tbBrand.Text = car.Brand;
                tbCarrying.Text = car.Carrying;
                cbDriver.ItemsSource = car.DRIVER + 1;
            }
        }

        public DepoWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ListCar = context.Car.ToList();
            cbDriver.ItemsSource = context.Car.ToList();
            cbDriver.DisplayMemberPath = "DRIVER";
            cbDriver.SelectedIndex = 0;
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

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validation())
                    return;


                Car car = new Car();

                car.NumberCar = tbNumber.Text;
                car.Brand = tbBrand.Text;
                car.DateMade = tbDateMade.SelectedDate.Value;
                car.Carrying = tbCarrying.Text;
                car.SpecEquipment = tbOther.Text;
                car.IDDriver = cbDriver.SelectedIndex + 1;

                MessageBox.Show("Автомобиль добавлен");
                context.Car.Add(car);
                context.SaveChanges();
                AllAboutCar.ItemsSource = context.Driver.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
        }

        private bool validation()
        {
            if (string.IsNullOrWhiteSpace(tbNumber.Text))
            {
                MessageBox.Show("Вы не ввели номер автомобиля");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbDateMade.Text))
            {

                MessageBox.Show("Вы не ввели дату изготовления");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbBrand.Text))
            {
                MessageBox.Show("Вы не ввели марку автомобиля");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbCarrying.Text))
            {
                MessageBox.Show("Вы не ввели грузоподъемность");
            }
            else if (string.IsNullOrWhiteSpace(cbDriver.Text))
            {
                MessageBox.Show("Вы не выбрали водителя");
            }
            return true;
        }

        private void btSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resClick = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.Yes)
                {
                    var result = context.Car.SingleOrDefault(b => b.IdCar== ((Car)selectedItemGrid).IdCar);
                    if (result != null)
                    {
                        result.NumberCar = tbNumber.Text;
                        result.DateMade = tbDateMade.SelectedDate.Value;
                        result.Brand = tbBrand.Text;
                        result.Carrying = tbCarrying.Text;
                        result.SpecEquipment = tbOther.Text;
                        result.IDDriver = cbDriver.SelectedIndex + 1;
                    }

                    context.SaveChanges();
                    MessageBox.Show("Пользователь успещно изменен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListCar = context.Car.ToList();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло нет так");
                return;
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var resClick = MessageBox.Show($"Удалить пользователя {(AllAboutCar.SelectedItem as EF.Car).Brand}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);

            try
            {
                if (resClick == MessageBoxResult.Yes)
                {
                    var result = context.Car.SingleOrDefault(b => b.IdCar== ((Car)selectedItemGrid).IdCar);
                    if (result == null)
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    else
                    {
                        context.Car.Remove(result);
                        context.SaveChanges();
                        MessageBox.Show("Водитель удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
            ListCar = context.Car.ToList();
        }
    }
}
