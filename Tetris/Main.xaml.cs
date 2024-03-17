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

namespace Tetris
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private Button btnC1 = new Button();
        private Button btnC2 = new Button();
        private Button btnC3 = new Button();
        private Button btnC4 = new Button();
        private List<Button> ListBtn = new List<Button>();
        public Main()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ListBtn.Add(btnC1);
            ListBtn.Add(btnC2);
            ListBtn.Add(btnC3);
            ListBtn.Add(btnC4);

            int i = 1;
            ListBtn.ForEach((button) =>
            {
                button.Content = String.Format("Câp độ {0}", i);
                button.Width = 211;
                button.Height = 52;
                button.Background = new SolidColorBrush(Colors.Orange);
                button.Margin = new Thickness(4);
                button.Visibility = Visibility.Hidden;
                button.Click += BtnClick;
                pnView.Children.Add(button);

                i++;
            });
        }

        private void playGame1(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = Visibility.Hidden;
            btn2PLay.Visibility = Visibility.Hidden;
            btnBangxephang.Visibility = Visibility.Hidden;

            ListBtn.ForEach((button) =>
            {
                button.Visibility = Visibility.Visible;
            });


        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;
            // Code to be executed when the button is clicked
            switch (clickedButton?.Content)
            {
                case "Câp độ 1":
                    var c1 = new MainWindow(22, 10, 250);
                    c1.Show();
                    Close();
                    break;
                case "Câp độ 2":
                    var c2 = new MainWindow(22, 10, 250, 40);
                    c2.Show();
                    Close();
                    break;
                case "Câp độ 3":
                    var c3 = new MainWindow(22, 16, 400);
                    c3.Show();
                    Close();
                    break;
                case "Câp độ 4":
                    var c4 = new MainWindow(22, 16, 400, 40);
                    c4.Show();
                    Close();
                    break;
            }
        }

        private void playGame2(object sender, RoutedEventArgs e)
        {
            var window2P = new _2Play();
            window2P.Show();
            Close();
        }

        private void btnBangxephang_Click(object sender, RoutedEventArgs e)
        {
            var window = new Rank();
            window.Show();
            Close();
        }
    }
}
