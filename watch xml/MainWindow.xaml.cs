using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using watch_xml.watcher;

namespace watch_xml
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFile of;
        Json json;
        string comboList = "broadcasting";
        int listSelectId=0;
        List<WatcherTwo> WTwo;
        List<WatcherOne> WOne;

        public MainWindow()
        {
            InitializeComponent();
            of = new OpenFile();
            json = new Json();
            json.read();

            WTwo = new List<WatcherTwo>();
            foreach (Retransmission ret in json.json.retransmission)
            {
                ret.status = "off";
                ret.artist = "";
                ret.name = "";
                WatcherTwo wt = new WatcherTwo();
                wt.dataFile = ret;
                wt.context = this;
                WTwo.Add(wt);
            }

            WOne = new List<WatcherOne>();
            foreach (Broadcasting ret in json.json.broadcasting)
            {
                ret.status = "off";
                ret.artist = "";
                ret.name = "";
                WatcherOne wo = new WatcherOne();
                wo.dataFile = ret;
                wo.context = this;
                WOne.Add(wo);
            }

            list_name_main.ItemsSource = json.json.broadcasting;
            
            List<string> cbeList = new List<string>() { "broadcasting", "retransmission" };
            ComboBoxElements cbe = new ComboBoxElements() { bindComboSearch = cbeList };
            comboBox_name_category.DataContext = cbe;
            comboBox_name_category.SelectedValue = "broadcasting";

            label_header.Visibility = Visibility.Hidden;
            button_header.Visibility = Visibility.Hidden;
            label_xml2Path.Visibility = Visibility.Hidden;
        }

        private void button_openPathXml(object sender, RoutedEventArgs e)
        {
            label_xmlPath.Content = of.open();
        }
        private void button_openPathXml2(object sender, RoutedEventArgs e)
        {
            label_xml2Path.Content = of.open();
        }
        private void button_openPathTxt(object sender, RoutedEventArgs e)
        {
            label_txtPath.Content = of.open();
        }

        private void list_main(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                listSelectId = listViewSelectedItems(list_name_main);
                if (comboList == "broadcasting")
                {
                    label_xmlPath.Content = json.json.broadcasting[listSelectId].xml;
                    label_txtPath.Content = json.json.broadcasting[listSelectId].txt;
                    label_err.Content = json.json.broadcasting[listSelectId].err;
                    label_status.Content = json.json.broadcasting[listSelectId].status;
                    start_name.Content = WOne[listSelectId].watcherXml ? "Stop" : "Start";
                }
                else if (comboList == "retransmission")
                {
                    label_xmlPath.Content = json.json.retransmission[listSelectId].xml;
                    label_xml2Path.Content = json.json.retransmission[listSelectId].xml2;
                    label_txtPath.Content = json.json.retransmission[listSelectId].txt;
                    label_err.Content = json.json.retransmission[listSelectId].err;
                    label_status.Content = json.json.retransmission[listSelectId].status;
                    start_name.Content = WTwo[listSelectId].watcherXml ? "Stop" : "Start";
                }
            }
            catch { }
        }

        
        private void start(object sender, RoutedEventArgs e)
        {
            if (comboList == "broadcasting")
            {
                if (!WOne[listSelectId].watcherXml)
                    WOne[listSelectId].Start();
                else
                    WOne[listSelectId].Stop();
                start_name.Content = WOne[listSelectId].watcherXml ? "Stop" : "Start";
                label_err.Content = json.json.broadcasting[listSelectId].err;
                label_status.Content = json.json.broadcasting[listSelectId].status;
            }
            else if (comboList == "retransmission")
            {
                if (!WTwo[listSelectId].watcherXml) 
                    WTwo[listSelectId].Start();
                else
                    WTwo[listSelectId].Stop();
                start_name.Content = WTwo[listSelectId].watcherXml ? "Stop" : "Start";
                label_err.Content = json.json.retransmission[listSelectId].err;
                label_status.Content = json.json.retransmission[listSelectId].status;
            }
        }

        


        private void ok(object sender, RoutedEventArgs e)
        {
            if (comboList == "broadcasting")
            {
                if (!WOne[listSelectId].watcherXml)
                {
                    json.json.broadcasting[listSelectId].xml = label_xmlPath.Content.ToString();
                    json.json.broadcasting[listSelectId].txt = label_txtPath.Content.ToString();
                    json.record();
                }
            }
            else if (comboList == "retransmission")
            {
                if (!WTwo[listSelectId].watcherXml)
                {
                    json.json.retransmission[listSelectId].xml = label_xmlPath.Content.ToString();
                    json.json.retransmission[listSelectId].xml2 = label_xml2Path.Content.ToString();
                    json.json.retransmission[listSelectId].txt = label_txtPath.Content.ToString();
                    json.record();
                }
            }
            list_name_main.Items.Refresh();
        }
        private void delete(object sender, RoutedEventArgs e)
        {
            if (comboList == "broadcasting")
            {
                if (!WOne[listSelectId].watcherXml)
                {
                    WOne.Remove(WOne[listSelectId]);
                    json.json.broadcasting.Remove(json.json.broadcasting[listSelectId]);
                    json.record();
                }
                else
                {
                    label_err.Content = "Остановите слежку за файлами";
                }
            }
            else if (comboList == "retransmission")
            {
                if (!WTwo[listSelectId].watcherXml)
                {
                    WTwo.Remove(WTwo[listSelectId]);
                    json.json.retransmission.Remove(json.json.retransmission[listSelectId]);
                    json.record();
                }
                else
                {
                    label_err.Content = "Остановите слежку за файлами";
                }
            }
            list_name_main.Items.Refresh();
        }
        private void newW(object sender, RoutedEventArgs e)
        {
            string xml = label_xmlPath.Content.ToString();
            string xml2 = label_xml2Path.Content.ToString();
            string txt = label_txtPath.Content.ToString();
            
            if (comboList == "broadcasting" && isPath() == 0)
            {
                Broadcasting br = new Broadcasting();
                br.xml = xml;
                br.txt = txt;
                json.json.broadcasting.Add(br);
                json.record();

                WatcherOne wo = new WatcherOne();
                wo.dataFile = br;
                wo.context = this;
                WOne.Add(wo);
            }
            else if (comboList == "retransmission" && isPath() == 0)
            {
                Retransmission re = new Retransmission();
                re.xml = xml;
                re.xml2 = xml2;
                re.txt = txt;
                json.json.retransmission.Add(re);
                json.record();

                WatcherTwo wt = new WatcherTwo();
                wt.dataFile = re;
                wt.context = this;
                WTwo.Add(wt);
            }
            list_name_main.Items.Refresh();
        }

        public int isPath()
        {
            string xml = label_xmlPath.Content.ToString();
            string xml2 = label_xml2Path.Content.ToString();
            string txt = label_txtPath.Content.ToString();

            bool isXml = xml != "" && File.Exists(xml);
            bool isXml2 = xml2 != "" && File.Exists(xml2);
            bool isTxt = txt != "" && File.Exists(txt);

            if (!isXml)
                return 1;
            if (!isTxt)
                return 2;
            if (!isXml2 && comboList == "retransmission")
                return 3;
            return 0;
        }

        private void comboBox_category(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Object selectedItem = comboBox_name_category.SelectedItem;
                comboList = selectedItem.ToString();
                //"broadcasting", "retransmission"
                if(comboList == "broadcasting")
                {
                    label_header.Visibility = Visibility.Hidden;
                    button_header.Visibility = Visibility.Hidden;
                    label_xml2Path.Visibility = Visibility.Hidden;
                    list_name_main.ItemsSource = json.json.broadcasting;
                }
                else if(comboList == "retransmission")
                {
                    label_header.Visibility = Visibility.Visible;
                    button_header.Visibility = Visibility.Visible;
                    label_xml2Path.Visibility = Visibility.Visible;
                    list_name_main.ItemsSource = json.json.retransmission;
                }
                list_name_main.Items.Refresh();
            }
            catch { }
        }
        private int listViewSelectedItems(ListView list)
        {
            try
            {
                int id = list.Items.IndexOf(list.SelectedItems[0]);
                if (id >= 0)
                    return id;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

        private void refrash(object sender, RoutedEventArgs e)
        {
            try
            {
                listSelectId = listViewSelectedItems(list_name_main);
                if (comboList == "broadcasting")
                {
                    label_xmlPath.Content = json.json.broadcasting[listSelectId].xml;
                    label_txtPath.Content = json.json.broadcasting[listSelectId].txt;
                    label_err.Content = json.json.broadcasting[listSelectId].err;
                    label_status.Content = json.json.broadcasting[listSelectId].status;
                    start_name.Content = WOne[listSelectId].watcherXml ? "Stop" : "Start";
                }
                else if (comboList == "retransmission")
                {
                    label_xmlPath.Content = json.json.retransmission[listSelectId].xml;
                    label_xml2Path.Content = json.json.retransmission[listSelectId].xml2;
                    label_txtPath.Content = json.json.retransmission[listSelectId].txt;
                    label_err.Content = json.json.retransmission[listSelectId].err;
                    label_status.Content = json.json.retransmission[listSelectId].status;
                    start_name.Content = WTwo[listSelectId].watcherXml ? "Stop" : "Start";
                }
            }
            catch { }
            list_name_main.Items.Refresh();
        }
        public bool isFull = true;
        private void mainWinBtn(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Escape")
            {
                this.WindowState = WindowState.Normal;
                full_name.Content = "Full";
                isFull = true;
            }
        }
        private void full(object sender, RoutedEventArgs e)
        {
            if (isFull)
            {
                this.WindowState = WindowState.Maximized;
                full_name.Content = "Normal";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                full_name.Content = "Full";
            }
            isFull = !isFull;
        }
        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void move_window(object sender, MouseButtonEventArgs e)//передвижение окна
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
