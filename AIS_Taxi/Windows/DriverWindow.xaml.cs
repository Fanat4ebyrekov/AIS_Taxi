using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



namespace AIS_Taxi.Windows
{
    /// <summary>
    /// Логика взаимодействия для DriverWindow.xaml
    /// </summary>
    public partial class DriverWindow : Window, INotifyPropertyChanged
    {
        

        private List<EF.Driver> listDriver;

        public List<EF.Driver> ListDriver
        {
            get { return listDriver; }
            set { 
                
                listDriver = value;

                OnPropertyChanged();


            }
        }

        private void Refresh()
        {
            ListDriver = context.Driver.ToList();
        }

        private object selectedItemGrid;

        public object SelectedItemGrid
        {

            get { return selectedItemGrid; }
            set {
                selectedItemGrid = value;
                OnPropertyChanged();

                Driver driver = (Driver)value;
                tbFIO.Text = driver.FIO;
                tbLicense.Text = driver.DriverLicense;
                tbPassport.Text = driver.Passport;
                tbPhone.Text = driver.Phone;
                tbAddress.Text = driver.Address;
            }
        }

        public DriverWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ListDriver = context.Driver.ToList();          
        }


        private void btnBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validation())
                    return;


                Driver driver = new Driver();

                string[] fioArr = tbFIO.Text.Split(' ');
                string[] PassArr = tbPassport.Text.Split(' ');

                driver.PassSeries = PassArr[0];
                driver.PassNum = PassArr[1];

                driver.LName = fioArr[0];
                driver.FName = fioArr[1];
                driver.Patronymic = fioArr[2];

                driver.Phone = tbPhone.Text;

                driver.DriverLicense = tbLicense.Text;

                driver.Address = tbAddress.Text;

                MessageBox.Show("Водитель добавлен");
                context.Driver.Add(driver);
                context.SaveChanges();
                AllAboutDriver.ItemsSource = context.Driver.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }    
        }

        private bool validation()
        {
            if (string.IsNullOrWhiteSpace(tbFIO.Text))
            {
                MessageBox.Show("Вы не ввели фамилию");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {

                MessageBox.Show("Вы не ввели телефон");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbLicense.Text))
            {
                MessageBox.Show("Вы не ввели лицензию");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                MessageBox.Show("Вы не ввели адрес");
            }
            else if (string.IsNullOrWhiteSpace(tbPassport.Text))
            {
                MessageBox.Show("Вы не ввели пасп. данные");
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
                    var result = context.Driver.SingleOrDefault(b => b.IdDriver == ((Driver)selectedItemGrid).IdDriver);
                    if (result != null)
                    {
                        string[] fioArr = tbFIO.Text.Split(' ');

                        result.LName = fioArr[0];
                        result.FName = fioArr[1];
                        result.Patronymic = fioArr[2];

                        result.Phone = tbPhone.Text;
                        result.DriverLicense = tbLicense.Text;
                        result.Address = tbAddress.Text;
                    }


                    context.SaveChanges();
                    MessageBox.Show("Пользователь успещно изменен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListDriver = context.Driver.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не так");
                return;
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var resClick = MessageBox.Show($"Удалить пользователя {(AllAboutDriver.SelectedItem as EF.Driver).LName}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);

            try
            {
                if (resClick == MessageBoxResult.Yes)
                {
                    var result = context.Driver.SingleOrDefault(b => b.IdDriver == ((Driver)selectedItemGrid).IdDriver);
                    if (result == null)
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    else
                    {
                        context.Driver.Remove(result);
                        context.SaveChanges();
                        MessageBox.Show("Водитель удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                }
              
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не так");
                return;
            }
            Refresh();
        }
    }
}

