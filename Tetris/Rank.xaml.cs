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
    /// Interaction logic for Rank.xaml
    /// </summary>
    public partial class Rank : Window
    {
        public Rank()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void backToMain(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new Main();
            newWindow.Show();
            Close();
        }

        private void changeOpaMouseMove(object sender, MouseEventArgs e)
        {
            if (back.Opacity == 0.6)
            {
                back.Opacity = 1;
            }
            else if (back.Opacity == 1)
            {
                back.Opacity = 0.6;
            }
        }
    }
}
