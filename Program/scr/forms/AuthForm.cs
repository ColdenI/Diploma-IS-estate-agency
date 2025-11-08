using Microsoft.Data.SqlClient;
using Program.scr.core;
using Program.scr.core.dbt;
using Program.scr.forms;

namespace Program
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
            if (!SQL.TestConnection()) MessageBox.Show("Ошибка подключения к БД!", "Ой...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button_reg_Click(object sender, EventArgs e)
        {
            this.Hide();
            new RegistrationForm().ShowDialog();
            this.Show();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string login = textBox_login.Text.Trim();
            string password = textBox_password.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT UserId FROM Users WHERE Login = @login AND Password = @password";
                        query.Parameters.AddWithValue("@login", login);
                        query.Parameters.AddWithValue("@password", password);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(0))
                                {
                                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                Core.ThisUser = DBT_Users.GetById(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка подключения к БД!", "Ой...", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            if(Core.ThisUser == null)
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(Core.ThisUser.Role == "Not")
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();
            if (Core.ThisUser.Role == "Client") new ClientForm().ShowDialog();
            else new ManagerForm().ShowDialog();
            Application.Exit();
        }
    }
}
