using System.Windows;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Test
{
    /// <summary>
    /// Interaction logic for AuthorizedWindow.xaml
    /// </summary>
    public partial class AuthorizedWindow : Window
    {
        private string connectionString;
        public AuthorizedWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Test.Properties.Settings.code_developmentConnectionString"].ConnectionString;
            this.Loaded += (sender, e) =>
            {
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        MessageBox.Show("Соединение установлено");

                        var data = new DataTable();
                        var adapter = new SqlDataAdapter("select * from dbo.Студенты", conn);
                        adapter.Fill(data);
                        dataGrid.ItemsSource = data.DefaultView;
                    }
                }
                catch
                {
                    MessageBox.Show("Соединение не установлено");
                }                
            };
        }
    }
}
