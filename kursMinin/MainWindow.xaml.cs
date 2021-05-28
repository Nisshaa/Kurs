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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursMinin
{
    public partial class Service
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
                var imageName = System.IO.Path.Combine(Environment.CurrentDirectory, img ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : new Uri("pack://application:,,,/img/picture.png");
            }
        }
        public float DiscountFloat
        {
            get
            {
                return Convert.ToSingle(Price ?? 0);
            }
        }
    } 
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private List<Service> _ServiceList;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Service> ServiceList
        {
            get
            {
                var FilteredServiceList = _ServiceList.FindAll(item =>
                item.DiscountFloat >= CurrentDiscountFilter.Item1 &&
              item.DiscountFloat < CurrentDiscountFilter.Item2);


                if (SortPriceAscending)
                    return FilteredServiceList.OrderBy(item => (item.Price)).ToList();
                else
                    return FilteredServiceList.OrderByDescending(item => (item.Price)).ToList();
            }
            set
            {
                _ServiceList = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ServiceList = Core.DB.Service.ToList();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void OrdProvidClick(object sender, RoutedEventArgs e)
        {
            var ord = new window.order();
            ord.ShowDialog();
        }

        private void SearchFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private Boolean _SortPriceAscending = true;
        public Boolean SortPriceAscending
        {
            get { return _SortPriceAscending; }
            set
            {
                _SortPriceAscending = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ServiceList"));
                }
            
            }
        
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortPriceAscending = (sender as RadioButton).Tag.ToString() == "1";
        }



        private Tuple<float, float> _CurrentDiscountFilter = Tuple.Create(float.MinValue, float.MaxValue);

        public Tuple<float, float> CurrentDiscountFilter
        {
            get
            {
                return _CurrentDiscountFilter;
            }
            set
            {
                _CurrentDiscountFilter = value;
                if (PropertyChanged != null)
                {
                    // при изменении фильтра список перерисовывается
                    PropertyChanged(this, new PropertyChangedEventArgs("ServiceList"));
                }
            }
        }

        public List<string> FilterByDiscountNamesList
        {
            get
            {
                return FilterByDiscountValuesList
                    .Select(item => item.Item1)
                    .ToList();
            }
        }

        private List<Tuple<string, float, float>> FilterByDiscountValuesList =
          new List<Tuple<string, float, float>>() {
        Tuple.Create("Все записи", 0f, 10000f),
        Tuple.Create("от 100 до 500", 0f, 500f),
        Tuple.Create("от 500 до 2000", 500f,2000f),
        Tuple.Create("от 2000 до 5000", 2000f, 10000f),
        Tuple.Create("от 5000 до 30000", 10000f, 50000f)
    };private void DiscountFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDiscountFilter = Tuple.Create(
                FilterByDiscountValuesList[DiscountFilterComboBox.SelectedIndex].Item2,
                FilterByDiscountValuesList[DiscountFilterComboBox.SelectedIndex].Item3
            );
        }

        
    }
}
