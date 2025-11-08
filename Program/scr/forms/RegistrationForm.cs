using Microsoft.Data.SqlClient;
using Program.scr.core;
using System.Text.RegularExpressions;

namespace Program.scr.forms
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button_reg_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(textBox_name.Text) ||
                    string.IsNullOrWhiteSpace(textBox_phone.Text) ||
                    string.IsNullOrWhiteSpace(textBox_email.Text) ||
                    string.IsNullOrWhiteSpace(textBox_login.Text) ||
                    string.IsNullOrWhiteSpace(textBox_password.Text) ||
                    string.IsNullOrWhiteSpace(textBox_password_agein.Text))
                {
                    MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Проверка длины имени
                if (textBox_name.Text.Length < 4)
                {
                    MessageBox.Show("Полное имя должно содержать более 3 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. Проверка длины логина
                if (textBox_login.Text.Length < 6)
                {
                    MessageBox.Show("Логин должен содержать более 5 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. Проверка совпадения паролей
                if (textBox_password.Text != textBox_password_agein.Text)
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 5. Проверка пароля: длина > 8, есть заглавная и строчная буква
                if (!IsValidPassword(textBox_password.Text))
                {
                    MessageBox.Show("Пароль должен быть длиннее 8 символов и содержать хотя бы одну заглавную и одну строчную букву.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 6. Проверка существования логина
                if (IsLoginExists(textBox_login.Text))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 7. Регистрация пользователя и клиента
                using (SqlConnection conn = new SqlConnection(SQL._sqlConnectStr))
                {
                    conn.Open();

                    // Начинаем транзакцию
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Вставляем в Users
                            string insertUserSql = @"
                                INSERT INTO Users (Login, Password, Role, CreatedAt) 
                                VALUES (@Login, @Password, 'Client', GETDATE());
                                SELECT SCOPE_IDENTITY();";

                            SqlCommand cmdUser = new SqlCommand(insertUserSql, conn, transaction);
                            cmdUser.Parameters.AddWithValue("@Login", textBox_login.Text);
                            cmdUser.Parameters.AddWithValue("@Password", textBox_password.Text);

                            object userIdObj = cmdUser.ExecuteScalar();
                            int userId = Convert.ToInt32(userIdObj);

                            // Вставляем в ClientProfiles
                            string insertClientSql = @"
                                INSERT INTO ClientProfiles (UserId, FullName, Phone, Email) 
                                VALUES (@UserId, @FullName, @Phone, @Email);";

                            SqlCommand cmdClient = new SqlCommand(insertClientSql, conn, transaction);
                            cmdClient.Parameters.AddWithValue("@UserId", userId);
                            cmdClient.Parameters.AddWithValue("@FullName", textBox_name.Text);
                            cmdClient.Parameters.AddWithValue("@Phone", textBox_phone.Text);
                            cmdClient.Parameters.AddWithValue("@Email", textBox_email.Text);

                            cmdClient.ExecuteNonQuery();

                            // Фиксируем транзакцию
                            transaction.Commit();

                            MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти в систему.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsValidPassword(string password)
        {
            if (password.Length < 9) return false;
            if (!Regex.IsMatch(password, @"[a-z]")) return false;
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;
            return true;
        }

        private bool IsLoginExists(string login)
        {
            using (SqlConnection conn = new SqlConnection(SQL._sqlConnectStr))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Users WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Login", login);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
