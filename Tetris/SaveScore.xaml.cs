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
    /// Interaction logic for SaveScore.xaml
    /// </summary>
    public partial class SaveScore : Window
    {
        public bool isSave = false;
        private int score;
        public SaveScore(int _score)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            score = _score;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt1.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên !!!", "Thông báo");
            }else
            {
                isSave = true;
                Rank.InsertData(txt1.Text, score);
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
