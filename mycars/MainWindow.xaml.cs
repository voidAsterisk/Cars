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

using System.IO;
namespace mycars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Parbaude vai eksiste darba fails
            // ja eksiste tad nolasit failu un pievienot watcher
            if (File.Exists("cars.csv"))
            {
                UpdateList(null, null);

                // Sis kods monetore failu vai taja nerodas izmanias un tad parveido sarakstu
                FileSystemWatcher fwatcher = new FileSystemWatcher();
                fwatcher.Path = ".";
                fwatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
                fwatcher.Changed += new FileSystemEventHandler(UpdateList);
                fwatcher.EnableRaisingEvents = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Open the add car menu
            addCarsWindow ac = new addCarsWindow();
            ac.Owner = this;
            ac.Show();
        }

        // Nonemt izveleto
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (carListBox.SelectedIndex != -1)
            {
                string tempFile = System.IO.Path.GetTempFileName();

                using (var sr = new StreamReader("cars.csv"))
                using (var sw = new StreamWriter(tempFile))
                {
                    string line;
                    int selectedIndex = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (selectedIndex != carListBox.SelectedIndex)
                            sw.WriteLine(line);
                        selectedIndex++;
                    }
                }
                
                File.Delete("cars.csv");
                File.Move(tempFile, "cars.csv");
                carListBox.Items.RemoveAt(carListBox.SelectedIndex);


            }
        }

        private void UpdateList(object sender, FileSystemEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                System.IO.StreamReader file = new System.IO.StreamReader("cars.csv");
                carListBox.Items.Clear();
                String line;
                while ((line = file.ReadLine()) != null)
                {
                    String[] strList = line.Split(',');
                    String myStr = "No " + strList[0] + " Lidz " + strList[1] + " " + strList[2] + " " + strList[3];
                    carListBox.Items.Add(myStr);
                }
                file.Close();
            });
        }
        
        
    }
}
