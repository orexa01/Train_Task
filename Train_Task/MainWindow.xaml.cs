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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Train_Task
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml http://www.itmathrepetitor.ru/zadachi-na-klassy/
    /// </summary>
    public partial class MainWindow : Window
    {
        Train_Poezd train;
        public int n = 5;
        
        public MainWindow()
        {
            InitializeComponent();
            Inicial();
        }
        private static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length - 1; j++)
                    if (array[j] < array[j + 1])
                    {
                        int t = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = t;
                    }
        }
        public void Inicial()
        {
            List<MyTable> result = new List<MyTable>();
            n = Convert.ToInt32(Value_n.Text);
            edt.Text = "";
            Random rnd = new Random();
            train.City = new string[n];
            train.Number = new int[n];
            train.Time = new string[n];

            for (int x = 0; x < n; x ++)
            {
                int rnd_int = rnd.Next(0, 5);
                string city = "";
                switch (rnd_int)
                {
                    case 0:
                        city = "Moskva";
                        break;
                    case 1:
                        city = "Saratov";
                        break;
                    case 2:
                        city = "Samara";
                        break;
                    case 3:
                        city = "Ufa";
                        break;
                    case 4:
                        city = "Penza";
                        break;
                    case 5:
                        city = "Tula";
                        break;
                }

                train.Number[x] = x + 1;
                train.City[x] = city;
                int hour = rnd.Next(0, 23);
                int min = rnd.Next(0, 59);
                train.Time[x] = hour.ToString() + ":" + min.ToString();
                Text_output(x);
                result.Add(item: new MyTable(train.Number[x], train.City[x], train.Time[x]));
            }
            grid.ItemsSource = result;
        }

        private void Train_Connect_Click(object sender, RoutedEventArgs e)
        {
            List<MyTable> result = new List<MyTable>();
            edt.Text = "";
            int[] value = new int[n];
            for (int x = 0; x < n; x++)
            {
                if(edtTrain.Text == train.City[x])
                {
                    value[x] = x;
                }
                else
                {
                    value[x] = -1;
                }
            }
            BubbleSort(value);
            for (int x = 0; x < n; x++)
            {
                if ((value[x] != -1) && (value[x + 1] != -1))
                {
                    for (int p = 0; p < n; p++)
                    {
                        if ((value[p] != -1) && (value[p + 1] != -1))
                        {
                            int val = Convert.ToInt32(train.Time[value[p]].Replace(":", ""));
                            int val_1 = Convert.ToInt32(train.Time[value[p + 1]].Replace(":", ""));
                            if (val > val_1)
                            {
                                int y = value[p + 1];
                                value[p + 1] = value[p];
                                value[p] = y;
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < n; x++)
            {
                if (value[x] != -1)
                {
                    Text_output(value[x]);
                    result.Add(item: new MyTable(train.Number[value[x]], train.City[value[x]], train.Time[value[x]]));
                }
            }
            grid.ItemsSource = result;



        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            Inicial();
        }

        private void reset_Click_1(object sender, RoutedEventArgs e)
        {
            List<MyTable> result = new List<MyTable>();
            edt.Text = "";
            for (int x = 0; x < n; x++)
            {
                Text_output(x);
                result.Add(item: new MyTable(train.Number[x], train.City[x], train.Time[x]));

            }
            grid.ItemsSource = result;
        }

        private void Value_Connect_Click(object sender, RoutedEventArgs e)
        {
            List<MyTable> result = new List<MyTable>();
            if (Convert.ToInt32(edtValue.Text) < n)
            {
                for (int x = 0; x < n; x++)
                {
                    if (Convert.ToInt32(edtValue.Text) == train.Number[x])
                    {
                        Text_output(x);
                        result.Add(item: new MyTable(train.Number[x], train.City[x], train.Time[x]));
                        break;
                    }
                }
            }
            grid.ItemsSource = result;

        }

        public void Text_output(int x)
        {
            edt.Text += train.Number[x].ToString() + ": " + train.City[x].ToString()
                        + " - " + train.Time[x].ToString() + "\n";
            
        }


    }


    public struct Train_Poezd
    {
        public string[] City;
        public int[] Number;
        public string[] Time;
    }


    public class MyTable
    {
        public MyTable(int Value, string City, string Time)
        {
            this.Value = Value;
            this.City = City;
            this.Time = Time;
        }
        public int Value { get; set; }
        public string City { get; set; }
        public string Time { get; set; }
    }

}
