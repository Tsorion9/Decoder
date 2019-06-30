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
using Decoder.BL;

namespace Decoder
{
    interface IMainWindow
    {
        String GetDecryptionMethod { get; }
        String GetKey { get; }
        String GetCipherText { get; }
        String DecryptedText { set; }
        List<String> Statistics { set; }
        event EventHandler DecipherClick;
    }

    public partial class MainWindow : Window, IMainWindow
    {
        TextBlock textBlockKey = new TextBlock() { Name = "textBlockKey", Height = 25, Width = 200, Text = "Введите ключ:",
        VerticalAlignment = VerticalAlignment.Top};
        TextBox textBoxKey = new TextBox() { Name = "textBoxKey", Height = 25, Width = 200};

        public MainWindow()
        {
            InitializeComponent();
            Manager manager = new Manager();
            Presenter presenter = new Presenter(this, manager);

            Grid.SetRow(textBlockKey, 0);
            Grid.SetColumn(textBlockKey, 1);
            Grid.SetRow(textBoxKey, 0);
            Grid.SetColumn(textBoxKey, 1);
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxStatistics.ItemsSource != null)
                listBoxStatistics.ItemsSource = null;
            if (DecipherClick != null)
                DecipherClick(this, e);
        }

        public String GetDecryptionMethod
        {
            get { return comBoxDecoder.Text; }
        }

        public String GetKey
        {
            get { return textBoxKey.Text; }
        }

        public String GetCipherText
        {
            get { return cipherText.Text; }
        }

        public String DecryptedText
        {
            set { decryptedText.Text = value; }
        }

        public List<String> Statistics
        {
            set { listBoxStatistics.ItemsSource = value; }
        }

        public event EventHandler DecipherClick;

        private void CaesarCipher_Selected(object sender, RoutedEventArgs e)
        {
            if (!grid.Children.Contains(textBoxKey))
            {
                grid.Children.Add(textBlockKey);
                grid.Children.Add(textBoxKey);
            }
        }

        private void CipherAtbash_Selected(object sender, RoutedEventArgs e)
        {
            if (!grid.Children.Contains(textBoxKey))
            {
                grid.Children.Add(textBlockKey);
                grid.Children.Add(textBoxKey);
            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            if (grid.Children.Contains(textBoxKey))
            {
                grid.Children.Remove(textBoxKey);
                grid.Children.Remove(textBlockKey);
            }
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            if (grid.Children.Contains(textBoxKey))
            {
                grid.Children.Remove(textBoxKey);
                grid.Children.Remove(textBlockKey);
            }
        }
    }
}