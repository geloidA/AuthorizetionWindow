using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using Test.Classes;

namespace Test
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private string connectionString;
        public RegistrationWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Test.Properties.Settings.code_developmentConnectionString"].ConnectionString;
            btRegistration.Click += BtRegistration_Click;
        }

        private void BtRegistration_Click(object sender, RoutedEventArgs e)
        {
            var checker = new PasswordChecker(6, "[0-9]", "[!@#$%^]", "[A-Za-z]");
            if (checker.Check(tBPassword.Text))
            {
                var query = "insert into dbo.Аккаунты (FIO, Password, Login) " +
                        $"values (\'{tBFIO.Text}\', \'{tBPassword.Text}\', \'{tBLogin.Text}\')";
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Сотрудник добавлен");
                }
            }
            else
                MessageBox.Show("Пароль должен содержать ! @ # $ % ^, как минимум 1 цифру, как минимум 1 заглавную букву");
        }
    }
}
