using System.Windows;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Test.Properties.Settings.code_developmentConnectionString"].ConnectionString;
            bRegister.Click += BRegister_Click;
            bLogin.Click += BLogin_Click;
        }

        private void BLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = "select * from dbo.Аккаунты " +
                        $"where dbo.Аккаунты.Login = {tBLogin.Text} " +
                        $"and dbo.Аккаунты.Password = {tBPassword.Text}";
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new SqlCommand(query, conn);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var fio = reader.GetValue(1).ToString();
                            MessageBox.Show($"Добро пожаловать {fio}");
                            var authorized = new AuthorizedWindow();
                            authorized.Show();
                        }
                    else
                        MessageBox.Show("Такого пользователя не существует");
                    reader.Close();
                }
            }
            catch
            {
                MessageBox.Show("Введите корректные данные");
            }
        }

        private void BRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegistrationWindow();
            registerWindow.Show();
        }
    }
}
