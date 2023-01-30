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

using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using static Azure.Core.HttpHeader;
using System.DirectoryServices;

namespace Заметки
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connect = @"Data Source = DESKTOP-JA41I9L; Initial Catalog = Notate; Trusted_connection=True";

        public string str = "SELECT * FROM  Notatka";
        string IDD;
        int kol=0;
        public MainWindow()
        {
            InitializeComponent();

            
        }
        async void Open()
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                texts.Text = "";
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(str, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                        
                    if (reader.HasRows)
                    {

                        string s1 = reader.GetName(0);
                        string s2 = reader.GetName(1);

                        

                        while (await reader.ReadAsync())
                        {
                            object id = reader.GetValue(0);
                            object id1 = reader.GetValue(1);

                            
                            texts.Text += id + "\t" + id1+"\n";

                            
                        }
                        await reader.CloseAsync();
                    }

                }
                catch (Exception e) { Console.WriteLine(e.Message); };

            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void texts_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Edite_Click(object sender, RoutedEventArgs e)
        {
            if (IDD != "")
            {
                string str2 = "DELETE FROM Notatka WHERE id = " + IDD;
                using (SqlConnection connection = new SqlConnection(connect))
                {
                    
                    try
                    {
                        await connection.OpenAsync();
                        SqlCommand command = new SqlCommand(str2, connection);
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                    
                    }
                    catch (Exception c) { Console.WriteLine(c.Message); };
                }
            }
        }
        private async void Searchh()
        {
            kol = 0;
            using (SqlConnection connection = new SqlConnection(connect))
            {

                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(str, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {

                        string s1 = reader.GetName(0);
                        string s2 = reader.GetName(1);

                        while (await reader.ReadAsync())
                        {
                            
                            object id = reader.GetValue(0);
                            object id1 = reader.GetValue(1);
                            
                            if (search.Text == id1.ToString())
                            {
                                search.Text = "";

                                search.Text = id + "\t" + id1;
                                kol=1;
                                IDD = id.ToString();

                                
                            }
                           
                        }
                        await reader.CloseAsync();
                    }

                }
                catch (Exception b) { Console.WriteLine(b.Message); };
                
                if(kol==0)
                {
                    Edite.Visibility = Visibility.Hidden;
                }
                else if(kol==1)
                {
                    Edite.Visibility = Visibility.Visible;
                }    
            }
            
        }
        private async void sear_Click(object sender, RoutedEventArgs e)
        {
            Searchh();
            
        }
    }
}
