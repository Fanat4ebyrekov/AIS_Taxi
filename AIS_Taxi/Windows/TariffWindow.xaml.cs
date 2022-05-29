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

namespace AIS_Taxi.Windows
{
    /// <summary>
    /// Логика взаимодействия для TarrifWindow.xaml
    /// </summary>
    public partial class TariffWindow : Window, INotifyPropertyChanged
    {

        private List<EF.Tariff> listTarrif;

        public List<EF.Tariff> ListTarrif
        {
            get { return listTarrif; }
            set
            {
                listTarrif = value;

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

                Tariff tariff = (Tariff)value;

                tbNameTariff.Text = tariff.NameTariff;
                tbDescription.Text = tariff.Description;
                tbPrice.Text = Convert.ToString(tariff.Pricekm);
            }
        }

        public TariffWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ListTarrif = context.Tariff.ToList();
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

        private void btSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resClick = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.Yes)
                {
                    var result = context.Tariff.SingleOrDefault(b => b.IdTarrif == ((Tariff)selectedItemGrid).IdTarrif);
                    if (result != null)
                    {
                        result.NameTariff = tbNameTariff.Text;
                        result.Description = tbDescription.Text;
                        //result.Pricekm = Convert.ToString(tbPrice.Text);
                    }

                    context.SaveChanges();
                    MessageBox.Show("Пользователь успещно изменен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListTarrif = context.Tariff.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validation())
                    return;



                    Tariff tariff = new Tariff();

                    tariff.NameTariff = tbNameTariff.Text;
                    tariff.Description = tbDescription.Text;
                    //tariff.Pricekm = Convert.ToString(tbPrice.Text);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
        }

        private bool validation()
        {
            if (string.IsNullOrWhiteSpace(tbNameTariff.Text))
            {
                MessageBox.Show("Вы не ввели новый тариф");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {

                MessageBox.Show("Вы не ввели описание тарифа");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbPrice.Text))
            {
                MessageBox.Show("Вы не ввели цену");
                return false;
            }
            return true;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var resClick = MessageBox.Show($"Удалить пользователя {(AllAboutTariff.SelectedItem as EF.Tariff).NameTariff}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);
            try
            {
                var result = context.Tariff.SingleOrDefault(b => b.IdTarrif == ((Tariff)selectedItemGrid).IdTarrif);
                if (result == null)
                {
                    MessageBox.Show("Тариф не выбран");
                    return;
                }
                else
                {
                    context.Tariff.Remove(result);
                    context.SaveChanges();
                    MessageBox.Show("Водитель удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
            ListTarrif = context.Tariff.ToList();
        }
    }
}
