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
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window, INotifyPropertyChanged
    {
        private List<EF.Worker> listWorker;

        public List<EF.Worker> ListWorker
        {
            get { return listWorker; }
            set
            {

                listWorker = value;

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

                Worker worker = (Worker)value;
                tbFIO.Text = worker.FIO;
                tbDateReciept.Text = Convert.ToString(worker.DateReciept.Date);
                tbPassport.Text = worker.Passport;
                tbPhone.Text = worker.Phone;
                tbAddress.Text = worker.Address;
            }
        }

        public WorkerWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ListWorker = context.Worker.ToList();
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
                    var result = context.Worker.SingleOrDefault(b => b.IdWorker == ((Worker)selectedItemGrid).IdWorker);
                    if (result != null)
                    {
                        string[] fioArr = tbFIO.Text.Split(' ');
                        string[] PassArr = tbPassport.Text.Split(' ');

                        result.PassSeries = PassArr[0];
                        result.PassNum = PassArr[1];

                        result.LName = fioArr[0];
                        result.FName = fioArr[1];
                        result.Patronymic = fioArr[2];

                        result.Phone = tbPhone.Text;
                        result.DateReciept = tbDateReciept.SelectedDate.Value;
                        result.Address = tbAddress.Text;

                    }

                    context.SaveChanges();
                    MessageBox.Show("Пользователь успещно изменен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListWorker = context.Worker.ToList();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло нет так");
                return;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validation())
                    return;


                Worker worker = new Worker();

                string[] fioArr = tbFIO.Text.Split(' ');
                string[] PassArr = tbPassport.Text.Split(' ');

                worker.PassSeries = PassArr[0];
                worker.PassNum = PassArr[1];

                worker.LName = fioArr[0];
                worker.FName = fioArr[1];
                worker.Patronymic = fioArr[2];

                worker.Phone = tbPhone.Text;

                worker.DateReciept = tbDateReciept.SelectedDate.Value;

                worker.Address = tbAddress.Text;

                MessageBox.Show("Сотрудник добавлен");
                context.Worker.Add(worker);
                context.SaveChanges();
                AllAboutWorker.ItemsSource = context.Driver.ToList();
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
                MessageBox.Show("Вы не ввели ФИО");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {

                MessageBox.Show("Вы не ввели телефон");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbDateReciept.Text))
            {
                MessageBox.Show("Вы не ввели дату поступления");
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

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var resClick = MessageBox.Show($"Удалить пользователя {(AllAboutWorker.SelectedItem as EF.Worker).FIO}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);

            try
            {
                if (resClick == MessageBoxResult.Yes)
                {
                    var result = context.Worker.SingleOrDefault(b => b.IdWorker == ((Worker)selectedItemGrid).IdWorker);
                    if (result == null)
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    else
                    {
                        context.Worker.Remove(result);
                        context.SaveChanges();
                        MessageBox.Show("Работник удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
            ListWorker = context.Worker.ToList();
        }
    }
}
