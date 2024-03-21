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
using System.Data.SqlClient;
using System.Data;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for Rank.xaml
    /// </summary>
    public partial class Rank : Window
    {
        public struct Score
        {
            public int Id;
            public string Ten;
            public int Diem;
        }

        List<Score> scoreList = new  List<Score>();


        public Rank()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ConnectDb();

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int i= 1;
            if(scoreList == null || scoreList.Count == 0)
            {
                lbl1.Content = String.Format("{0} ({1})", "Ngoc", 20);
                lbl2.Content = String.Format("{0} ({1})", "Chuong", 18);
                lbl3.Content = String.Format("{0} ({1})", "Nam", 16);
            } else
            {
                foreach (Score s in scoreList)
                {
                    if (i == 1) lbl1.Content = String.Format("{0} ({1})", s.Ten, s.Diem);
                    if (i == 2) lbl2.Content = String.Format("{0} ({1})", s.Ten, s.Diem);
                    if (i == 3) lbl3.Content = String.Format("{0} ({1})", s.Ten, s.Diem);

                    i++;
                }
            }

        }

        private void ConnectDb()
        {
            string connectionString = "Data Source=DESKTOP-BN2K0OH\\SQLEXPRESS;" +
                                        "Initial Catalog=tetris;" +
                                        "Integrated Security=SSPI;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TOP 3 * FROM score ORDER BY diem DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                scoreList.Add(new Score { Id = reader.GetInt32(0), Ten = reader.GetString(1), Diem = reader.GetInt32(2) });
                            }
                        }
                    }
                    // Execute SQL statements or perform database operations here
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                // Handle connection errors
                Console.WriteLine(ex);
            }
        }

        public static void InsertData(string name, int diem)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-BN2K0OH\\SQLEXPRESS;" +
                                      "Initial Catalog=tetris;" +
                                      "Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlCommand = "INSERT INTO score (name, diem) VALUES (@name, @diem)";

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@diem", diem);
                    command.ExecuteNonQuery();
                }
            }
            }
            catch (SqlException ex)
            {
                // Handle connection errors
                Console.WriteLine(ex);
            }
        }

    }
}
