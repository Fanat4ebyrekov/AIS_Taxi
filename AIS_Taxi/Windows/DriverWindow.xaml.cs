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
using AIS_Taxi.EF;



namespace AIS_Taxi.Windows
{
    /// <summary>
    /// Логика взаимодействия для DriverWindow.xaml
    /// </summary>
    public partial class DriverWindow : Window, INotifyPropertyChanged
    {
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
            AllAboutDriver.ItemsSource = Clasess.Entities.context.Driver.ToList();

            this.DataContext = this;
        }


        private void btnBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void tbFIO_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Driver user = AllAboutDriver.SelectedItem as Driver;
            //var FIO = FIO.Where
            //tbFIO.Text = user.ToString();
            //MessageBox.Show("dfdfdf");
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

                driver.FName = fioArr[0];
                driver.LName = fioArr[1];
                driver.Patronymic = fioArr[2];

                driver.Phone = tbPhone.Text;

                driver.DriverLicense = tbLicense.Text;

                driver.Address = tbAddress.Text;

                MessageBox.Show("User Add");
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
            Driver driver = new Driver();

            try
            {
                var SaveEdit = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (SaveEdit == MessageBoxResult.Yes)
                {
                    string[] fioArr = tbFIO.Text.Split(' ');
                    string[] PassArr = tbPassport.Text.Split(' ');

                    driver.PassSeries = PassArr[0];
                    driver.PassNum = PassArr[1];

                    driver.FName = fioArr[0];
                    driver.LName = fioArr[1];
                    driver.Patronymic = fioArr[2];

                    driver.Phone = tbPhone.Text;

                    driver.DriverLicense = tbLicense.Text;

                    driver.Address = tbAddress.Text;
                }

                Clasess.Entities.context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
            

        }
    }
}
