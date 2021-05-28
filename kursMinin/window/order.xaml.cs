using System;
using System.Collections.Generic;
using System.ComponentModel;
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


namespace kursMinin
{
    public partial class Userman
    {
        // ссылка на картинку
        // по ТЗ, если картинка не найдена, то должна выводиться картинка по-умолчанию
        // в XAML-е можно это сделать средствами разметки, но там есть условие что вместо ссылки на картинку получен NULL
        // у нас же возможна ситуация, когда в базе есть путь к картинке, но самой картинки в каталоге нет
        // поэтому я сделал проверку наличия файла картинки и возвращаю картинку по-умолчанию, если нужной нет 
        public Uri ImagePreview
        {
            get
            {
                var imageName = System.IO.Path.Combine(Environment.CurrentDirectory, photo ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : new Uri("pack://application:,,,/img/picture.png");
            }
        }
    }
}
namespace kursMinin.window
{
    /// <summary>
    /// Логика взаимодействия для order.xaml
    /// </summary>
    public partial class order : Window, INotifyPropertyChanged
    {
        private List<Zakazy> _OrderList;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Zakazy> OrderList
            {
            get
            {
                return _OrderList;
            }
            set
            {
                _OrderList = value;
                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs("OrderList"));
                }
            }
        }

        public order()
        {
            InitializeComponent();
            this.DataContext = this;
            OrderList = Core.DB.Zakazy.ToList();
        }
        private void DelOrd_Click(object sender, RoutedEventArgs e)
        {
            var item = ProductListView.SelectedItem as Zakazy;
            Core.DB.Zakazy.Remove(item);
            Core.DB.SaveChanges();
            OrderList = Core.DB.Zakazy.ToList();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            var SelectedService = ProductListView.SelectedItem as Zakazy;
            var EditServiceWindow = new window.ServiceWindow(SelectedService);
            if ((bool)EditServiceWindow.ShowDialog())
            {
                // при успешном завершении не забываем перерисовать список услуг
                PropertyChanged(this, new PropertyChangedEventArgs("OrderList"));
                // и еще счетчики - их добавьте сами
            }
        }
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            //  создаем новую услугу
            var NewService = new Zakazy();

            var NewServiceWindow = new ServiceWindow(NewService);
            if ((bool)NewServiceWindow.ShowDialog())
            {
                //список услуг нужно перечитать с сервера
                OrderList = Core.DB.Zakazy.ToList();
              
                PropertyChanged(this, new PropertyChangedEventArgs("OrderList"));
            }
        }
    }
}