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
using System.Windows.Shapes;

using System.IO;

namespace mycars
{
    /// <summary>
    /// Interaction logic for addCarsWindow.xaml
    /// </summary>
    public partial class addCarsWindow : Window
    {
        public addCarsWindow()
        {
            InitializeComponent();

            /* 0 - 24 lai uzradas */
            for (int i = 1; i < 25; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                comboBox.Items.Add(i);
            }
            for (int i = 1; i < 25; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                comboBox_Copy.Items.Add(i);
            }
        }

        // Atcelt poga
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Pievienot poga
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool NoErrors = true;
            // Parbaudit vai visas vertibas ir ievaditas.
            if (comboBox.SelectedIndex == -1)
            {
                comboBox.Focus();
                NoErrors = false;
            }
            if (comboBox_Copy.SelectedIndex == -1)
            {
                comboBox_Copy.Focus();
                NoErrors = false;
            }
            if (nameBox.Text == "")
            {
                nameBox.Focus();
                NoErrors = false;
            }
            if (carNumberBox.Text == "")
            {
                carNumberBox.Focus();
                NoErrors = false;
            }

            /*
            int from = (int)comboBox.SelectedItem;
            int to = (int)comboBox.SelectedItem;
            */

            if (NoErrors)
            {
                // Ierakstit datus faila
                using (StreamWriter w = File.AppendText("cars.csv"))
                {
                    // Prveidot ievaditas vertibas CSV faila formata
                    String fout = "";
                    fout += comboBox.SelectedItem.ToString() + ",";
                    fout += comboBox_Copy.SelectedItem.ToString() + ",";
                    fout += nameBox.Text + ",";
                    fout += carNumberBox.Text + "\r\n";


                    w.Write(fout);
                }

                // Aizvert logu
                this.Close();
            }
        }
    }
}
