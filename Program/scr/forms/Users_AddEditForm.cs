using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Users_AddEditForm : Form
    {
        DBT_Users Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_Login;
        TextBox textBox_Password;
        ComboBox comboBox_Role;
        DateTimePicker dateTimePicker_CreatedAt;

        public Users_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Users_AddEditForm(DBT_Users obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_Login.Text = obj.Login.ToString();
            textBox_Password.Text = obj.Password.ToString();
            comboBox_Role.Text = obj.Role.ToString();
            //dateTimePicker_CreatedAt.Value = (DateTime)((obj.CreatedAt == null) ? DateTime.Now : obj.CreatedAt);
        }

        private void Init()
        {
            this.Size = new Size(500, 300);
            this.Text = "Пользователи - " + (Object == null ? "Добавить" : "Изменить").ToString();
            this.MinimumSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            button_apply = new Button()
            {
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_apply.Click += Button_apply_Click;
            button_apply.Text = Object == null ? "Добавить" : "Изменить";
            this.Controls.Add(button_apply);

            tableLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4
            };

            Label label_Login = new Label();
            SetLabel(ref label_Login, "Логин");
            tableLayout.Controls.Add(label_Login, 0, 0);
            textBox_Login = new TextBox();
            textBox_Login.Dock = DockStyle.Fill;
            textBox_Login.MaxLength = 50;
            tableLayout.Controls.Add(textBox_Login, 1, 0);

            Label label_Password = new Label();
            SetLabel(ref label_Password, "Пароль");
            tableLayout.Controls.Add(label_Password, 0, 1);
            textBox_Password = new TextBox();
            textBox_Password.Dock = DockStyle.Fill;
            textBox_Password.MaxLength = 50;
            tableLayout.Controls.Add(textBox_Password, 1, 1);

            Label label_Role = new Label();
            SetLabel(ref label_Role, "Роль");
            tableLayout.Controls.Add(label_Role, 0, 2);
            comboBox_Role = new ComboBox();
            comboBox_Role.Items.Add("Not");
            comboBox_Role.Items.Add("Admin");
            comboBox_Role.Items.Add("Manager");
            comboBox_Role.Items.Add("Client");
            comboBox_Role.Dock = DockStyle.Fill;
            comboBox_Role.MaxLength = 20;
            tableLayout.Controls.Add(comboBox_Role, 1, 2);

            Label label_CreatedAt = new Label();
            SetLabel(ref label_CreatedAt, "Дата создания");
            //tableLayout.Controls.Add(label_CreatedAt, 0, 3);
            dateTimePicker_CreatedAt = new DateTimePicker();
            dateTimePicker_CreatedAt.Dock = DockStyle.Fill;
            //tableLayout.Controls.Add(dateTimePicker_CreatedAt, 1, 3);
            dateTimePicker_CreatedAt.Format = DateTimePickerFormat.Custom;
            dateTimePicker_CreatedAt.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            this.Controls.Add(tableLayout);
        }

        private void SetLabel(ref Label label, string text = "")
        {
            label.Font = new Font(Font.FontFamily, 12);
            label.TextAlign = ContentAlignment.TopLeft;
            label.Dock = DockStyle.Fill;
            label.AutoSize = false;
            label.Width = 200;
            label.Text = text;
        }

        private void Button_apply_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_Login.Text)) { MessageBox.Show("Поле 'Логин' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Password.Text)) { MessageBox.Show("Поле 'Пароль' имеет некорректное значение!"); return; }
            if (comboBox_Role.SelectedIndex == -1) { MessageBox.Show("Поле 'Роль' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Users.Create(
                    new DBT_Users()
                    {
                        Login = textBox_Login.Text,
                        Password = textBox_Password.Text,
                        Role = comboBox_Role.Items[comboBox_Role.SelectedIndex].ToString(),
                        CreatedAt = dateTimePicker_CreatedAt.Value
                    }
                );
            }
            else
            {
                res = DBT_Users.Edit(
                    new DBT_Users()
                    {
                        UserId = Object.UserId,
                        Login = textBox_Login.Text,
                        Password = textBox_Password.Text,
                        Role = comboBox_Role.Items[comboBox_Role.SelectedIndex].ToString(),
                        CreatedAt = Object.CreatedAt
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
