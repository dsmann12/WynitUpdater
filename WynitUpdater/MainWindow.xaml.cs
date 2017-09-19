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
using Microsoft.Win32;
using System.IO;

namespace WynitUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filename;
        public MainWindow()
        {
            WynitUpdate.LoadSkus();
            InitializeComponent();
            this.textBox.Text = Directory.GetCurrentDirectory();
            this.textBox.TextWrapping = TextWrapping.NoWrap;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.ShowDialog();
            filename = d.FileName;
            this.textBox.Text = filename;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if(filename == null)
            {
                MessageBox.Show("No file was selected!");
            }
            else
            {
                try
                {
                    WynitUpdate.ReadWynitFile(filename);
                    WynitUpdate.GenerateFile();
                    MessageBox.Show("Done!"); 
                }
                catch(IndexOutOfRangeException ex)
                {
                    MessageBox.Show("A problem occurred. Make sure the correct file was selected");
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show("A porblem occurred. Make sure the correct file was selected ");
                }
            }
        }
    }
}
