
<table style="width: 100%;">
  <tr>
    <td style="text-align: center; border: none;"> 
        Министерство образования и науки РФ <br/>
        ГБПОУ РМЭ "Йошкар-Олинский Технологический колледж 
    </td>
  </tr>
  <tr>
    <td style="text-align: center; border: none; height: 45em;">
        <h2>
            Курсовой проект <br/>
            "Проектирование и разработка информационных систем" <br/>
            для группы И-31
        <h2>
    </td>
  </tr>
  <tr>
    <td style="text-align: right; border: none; height: 20em;">
        <div style="float: right;" align="left">
            <b>Разработал</b>: <br/>
           Минин Дмитрий Николаевич <br/>
            <b>Проверил</b>: <br/>
            Колесников Евгений Иванович
        </div>
    </td>
  </tr>
  <tr>
    <td style="text-align: center; border: none; height: 1em;">
        г.Йошкар-Ола, 2021
    </td>
  </tr>
</table>

<div style="page-break-after: always;"></div>

https://github.com/Nisshaa/Kurs

# Содержание

* [Теоретическая часть](#Теоретическая-часть)



# Теоретическая-часть
## Диаграммы

### Предметная область  Фитнес-центр.Подсистема работы с сотрудниками.

### Диаграмма Use Case:

![Use case](./img/UseCase.png)


### Диаграмма ER:
![ER диаграмма](./img/ERD.png)



# Практическая часть
## Программирование С#


### Приложение было разработанно в Visual Studio, пример работы программы:
### Главное окно:
![MainWindow](./img/MainWindow.PNG)
#### Прмер кода разметки страницы:
  <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="150"/>
            <ColumnDefinition Width="1*"/>
        </Grid.ColumnDefinitions>
        <Image 
        Margin="5"
        Source="./Logo/Logo.jpg" 
        VerticalAlignment="Top"/>
        <StackPanel VerticalAlignment="Bottom">

            <Label Content="Цена: "/>
            <RadioButton 
                GroupName="Price"
                Tag="1"
                Content="по возрастанию" 
                IsChecked="True" 
                Checked="RadioButton_Checked"
                VerticalContentAlignment="Center"/>
            <RadioButton 
                GroupName="Price" 
                Tag="2"
                Content="по убыванию" 
                Checked="RadioButton_Checked"
                VerticalContentAlignment="Center"/>

            <Label Content="Фильтр по скидке: "
        Margin="10,0,0,0"
        VerticalAlignment="Center"/>
            <ComboBox
    Name="DiscountFilterComboBox"
    SelectedIndex="0"
    SelectionChanged="DiscountFilterComboBox_SelectionChanged"
    ItemsSource="{Binding FilterByDiscountNamesList}"/>
            <Button Margin="5" x:Name="OrderProvid" Content="Заказы" Click="OrdProvidClick"></Button>
            <Button Margin="5" x:Name="ExitBtn" Content="Выход" Click="ExitButtonClick"></Button>
        </StackPanel>
      
        <ListView
            Grid.Row="1"
            Grid.Column="1"
            ItemsSource="{Binding ServiceList}"
            x:Name="ProductListView">
            <ListView.ItemContainerStyle>
                <Style 
                    TargetType="ListViewItem">
                    <Setter 
                        Property="HorizontalContentAlignment"
                        Value="Stretch" />
                </Style>
            </ListView.ItemContainerStyle>
            <ListView.ItemTemplate>
                <DataTemplate>
                    <!-- рисуем вокруг элемента границу с загругленными углами -->
                    <Border 
                BorderThickness="1" 
                BorderBrush="Black" 
                CornerRadius="5">
                        <!-- основная "сетка" из 3-х столбцов: картинка, содержимое, цена -->
                        <Grid 
                    Margin="10" 
                    HorizontalAlignment="Stretch">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="64"/>
                                <ColumnDefinition Width="*"/>
                                <ColumnDefinition Width="100"/>
                            </Grid.ColumnDefinitions>
                            <Image
                        Width="64" 
                        Height="64"
                        Source="{Binding Path=ImagePreview}" />
                            <TextBlock 
                        Text="{Binding Price}" 
                        Grid.Column="2" 
                        HorizontalAlignment="Right" 
                        Margin="10"/>
                            <!-- для содержимого рисуем вложенную сетку -->
                            <Grid Grid.Column="1" Margin="5">
                                <Grid.RowDefinitions>
                                    <RowDefinition Height="20"/>
                                    <RowDefinition Height="20"/>
                                    <RowDefinition Height="*"/>
                                </Grid.RowDefinitions>
                                <StackPanel
                            Orientation="Horizontal">
                                    <TextBlock 
                                Text="Время улсуги-"/>
                                    <TextBlock 
                                Text="{Binding Duration}"/>
                                    <TextBlock 
                                Text="  ч | "/>
                                    <TextBlock 
                                Text="{Binding Title}"/>
                                </StackPanel>
                            </Grid>
                        </Grid>
                    </Border>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </Grid>
</Window>
```


#### Пример Логики главной страницы:

```cs
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
```

### Окно заказов:
![OrderWindow](./img/OrderWindow.png)
#### Прмер кода разметки страницы заказов:
```xml
 <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="150"/>
            <ColumnDefinition Width="1*"/>
        </Grid.ColumnDefinitions>

        <StackPanel VerticalAlignment="Bottom">
            <Button Margin="5" x:Name="CreateOrdBtn" Content="Создание заказа" Click="AddOrder_Click"></Button>
            <Button Margin="5" x:Name="EditOrdBtn" Content="Изменение Заказа" Click="EditOrder_Click"></Button>
            <Button Margin="5" x:Name="DelOrdBtn" Content="Удаление Заказа" Click="DelOrd_Click"></Button>
        </StackPanel>

        <ListView
            Grid.Row="1"
            Grid.Column="1"
            ItemsSource="{Binding OrderList}"
            x:Name="ProductListView">
            <ListView.ItemContainerStyle>
                <Style 
                    TargetType="ListViewItem">
                    <Setter 
                        Property="HorizontalContentAlignment"
                        Value="Stretch" />
                </Style>
            </ListView.ItemContainerStyle>
            <ListView.ItemTemplate>
                <DataTemplate>
                    <!-- рисуем вокруг элемента границу с загругленными углами -->
                    <Border 
                BorderThickness="1" 
                BorderBrush="Black" 
                CornerRadius="5">
                        <!-- основная "сетка" из 3-х столбцов: картинка, содержимое, цена -->
                        <Grid 
                    Margin="10" 
                    HorizontalAlignment="Stretch">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="64"/>
                                <ColumnDefinition Width="*"/>
                                <ColumnDefinition Width="100"/>
                            </Grid.ColumnDefinitions>

                            <Image
                        Width="64" 
                        Height="64"
                        Source="{Binding Userman.ImagePreview}" />
                            <!-- ,TargetNullValue={StaticResource DefaultImage} -->

                            <TextBlock 
                        Text="{Binding Number}" 

                        Grid.Column="2" 
                        HorizontalAlignment="Right" 
                        Margin="10"/>

                            <!-- для содержимого рисуем вложенную сетку -->
                            <Grid Grid.Column="1" Margin="5">
                                <Grid.RowDefinitions>
                                    <RowDefinition Height="20"/>
                                    <RowDefinition Height="20"/>
                                    <RowDefinition Height="*"/>
                                </Grid.RowDefinitions>

                                <StackPanel
                            Orientation="Horizontal">
                                    <TextBlock 
                                Text="{Binding Userman.FirstName}"/>
                                    <TextBlock 
                                Text="{Binding Userman.LastName}"/>
                                    <TextBlock 
                                Text="{Binding Userman.Phone}"/>
                                    <TextBlock 
                                Text=" | "/>
                                    <TextBlock 
                                Text="{Binding Total}"/>
                                </StackPanel>

                                <TextBlock 
                            Text="{Binding Data}" 
                            Grid.Row="1"/>
                            </Grid>
                        </Grid>
                    </Border>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </Grid>
</Window>
```
#### Пример Логики страницы заказов:

```cs
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
```
### Окно добавления и редактирования заказов:
![ServiceWindow](./img/EditOrder.png)
![ServiceWindow](./img/Red.PNG)
#### Прмер кода разметки страницы редактирования заказов:
```xml
  <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="auto"/>
            <ColumnDefinition  Width="*"/>
        </Grid.ColumnDefinitions>
      
        <StackPanel Grid.Column="1" Orientation="Horizontal" Visibility="{Binding NewProduct}">
            <Label Content="Идентификатор услуги: "/>
            <Label Content="{Binding CurrentOrder.id}"/>
        </StackPanel>

        <StackPanel Margin="5 60" Grid.Column="1">
            <ComboBox
                HorizontalAlignment="left"
                ItemsSource="{Binding UsermanList}"
                SelectedItem="{Binding CurrentOrder.Userman}">
                <ComboBox.ItemTemplate>
                    <DataTemplate>
                        <Label Content="{Binding FullName}" Visibility="{Binding FilterRole}"/>
                    </DataTemplate>
                </ComboBox.ItemTemplate>
            </ComboBox>

            <Label Content="Номер заказа"/>
            <TextBox Text="{Binding CurrentOrder.Number}"/>
            <Label Content="Дата заказа"/>
            <TextBox Text="{Binding CurrentOrder.Data}"/>
            <Label Content="Цена заказа"/>
            <TextBox Text="{Binding CurrentOrder.Total}"/>
            <Label Content="Время заказа"/>
            <TextBox Text="{Binding CurrentOrder.OrderTime}"/>
            <Button Width="100" Margin="5" HorizontalAlignment="Left" Click="SaveButton_Click">Сохранить</Button>
        </StackPanel>
    </Grid>
</Window>

    ```
#### Пример Логики страницы редактирования заказов:

```cs
namespace kursMinin
{
    public partial class Userman
    {
        public string FullName
        {
            get
            {
                return FirstName+LastName+Patronomyc;
            }
        }
        public string FilterRole
        {
            get
            {
                if (Roleid == 4)
                {
                    return "Visible";
                }
                return "Collapsed";
               
                                
            }
        }
    }
}


namespace kursMinin.window
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window, INotifyPropertyChanged
    {

        public List<Userman> UsermanList { get; set; }

        public ServiceWindow(Zakazy zakazy)
        {
            InitializeComponent();
            this.DataContext = this;
            CurrentOrder = zakazy;
            UsermanList = Core.DB.Userman.ToList();

        }
        public Zakazy CurrentOrder { get; set; }
        public string WindowName
        {
            get
            {
                return CurrentOrder.id == 0 ? "Новая услуга" : "Редоктирование улсгуи";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {



            {
                if (CurrentOrder.id == 0)
                    Core.DB.Zakazy.Add(CurrentOrder);

                
                try
                {
                    Core.DB.SaveChanges();
                }
                catch
                {
                }
                DialogResult = true;
            }
        
           
           
        }
       
        public string NewProduct
        {
            get
            {
                if (CurrentOrder.id == 0) return "collapsed";
                return "visible";



            }
        }
    }
}
```
# Тестировние
## Создание библиотеки классов и Юнит тестов
### Библиотека классов
```cs
namespace CompanyCoreLib
{
    public class Analytics
    {

        public Boolean CheckPrice(int price)
        {
            if (price <= 2000)
            {
                return false;
            }
            else
                return true;
        }

        public int DiscountPrice(int price)
        {
            int sum = ((price / 100) * 15) * price;
            return sum;
        }

        public string PriceNotNull(int price)
        {
            if (price <= 0)
            {
                return "Цена не может быть меньше или равна нулю!!!";
            }
            else
                return "Всё правильно";
        }
    }
}


```
### Юнит тесты 

```cs
namespace UnitTestProject
{
    [TestClass]
    public class UnitTestProject1
    {
        static Analytics disk_price;
        [ClassInitialize]
        static public void Init(TestContext tc)
        {
            disk_price = new Analytics();
        }


        //Проверка на размер цены
        [TestMethod]
        public void CheckPrice()
        {
            Assert.IsTrue(disk_price.CheckPrice(5000));
        }


     


        //Проверка на тип данных
        [TestMethod]
        public void ValidationPrice()
        {
            Assert.IsInstanceOfType(disk_price.DiscountPrice(100), typeof(int));
        }

        //Проверка на нулевую цену
        [TestMethod]
        public void PriceNotNull()
        {
            Assert.AreEqual(disk_price.PriceNotNull(-12), "Цена не может быть меньше или равна нулю!!!");
        }
    }
}
```
## Пример работы юнит тестов:
![EditorderWindow](./img/Unit_Tests.png)
