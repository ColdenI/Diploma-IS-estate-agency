using Program.scr.core.dbt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program.scr.forms
{
    public partial class ManagerProfiles_AddEditForm : Form
    {
        DBT_ManagerProfiles Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_UserId;
        TextBox textBox_FullName;
        TextBox textBox_Phone;
        TextBox textBox_LicenseNumber;
        TextBox textBox_Email;
        TextBox textBox_Post;
        DateTimePicker dateTimePicker_HireDate;

        public ManagerProfiles_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public ManagerProfiles_AddEditForm(DBT_ManagerProfiles obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_UserId.Text = obj.UserId.ToString();
            textBox_FullName.Text = obj.FullName.ToString();
            textBox_Phone.Text = obj.Phone.ToString();
            textBox_LicenseNumber.Text = obj.LicenseNumber.ToString();
            textBox_Email.Text = obj.Email.ToString();
            textBox_Post.Text = obj.Post.ToString();
            dateTimePicker_HireDate.Value = (DateTime)((obj.HireDate == null) ? DateTime.Now : obj.HireDate);
        }

        private void Init()
        {
            this.Size = new Size(1200, 650);
            this.Text = "Менеджеры - " + (Object == null ? "Добавить" : "Изменить").ToString();
            this.MinimumSize = new Size(400, 400);
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
                RowCount = 7
            };

            Label label_UserId = new Label();
            SetLabel(ref label_UserId, "ID Пользователя");
            //tableLayout.Controls.Add(label_UserId, 0, 0);
            textBox_UserId = new TextBox();
            textBox_UserId.Dock = DockStyle.Fill;
            textBox_UserId.MaxLength = 1000;
            //tableLayout.Controls.Add(textBox_UserId, 1, 0);

            Label label_FullName = new Label();
            SetLabel(ref label_FullName, "ФИО");
            tableLayout.Controls.Add(label_FullName, 0, 1);
            textBox_FullName = new TextBox();
            textBox_FullName.Dock = DockStyle.Fill;
            textBox_FullName.MaxLength = 100;
            tableLayout.Controls.Add(textBox_FullName, 1, 1);

            Label label_Phone = new Label();
            SetLabel(ref label_Phone, "Номер телефона");
            tableLayout.Controls.Add(label_Phone, 0, 2);
            textBox_Phone = new TextBox();
            textBox_Phone.Dock = DockStyle.Fill;
            textBox_Phone.MaxLength = 20;
            tableLayout.Controls.Add(textBox_Phone, 1, 2);

            Label label_LicenseNumber = new Label();
            SetLabel(ref label_LicenseNumber, "Лицензия");
            tableLayout.Controls.Add(label_LicenseNumber, 0, 3);
            textBox_LicenseNumber = new TextBox();
            textBox_LicenseNumber.Dock = DockStyle.Fill;
            textBox_LicenseNumber.MaxLength = 50;
            tableLayout.Controls.Add(textBox_LicenseNumber, 1, 3);

            Label label_Email = new Label();
            SetLabel(ref label_Email, "Электронная почта");
            tableLayout.Controls.Add(label_Email, 0, 4);
            textBox_Email = new TextBox();
            textBox_Email.Dock = DockStyle.Fill;
            textBox_Email.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Email, 1, 4);

            Label label_Post = new Label();
            SetLabel(ref label_Post, "Должность");
            tableLayout.Controls.Add(label_Post, 0, 5);
            textBox_Post = new TextBox();
            textBox_Post.Dock = DockStyle.Fill;
            textBox_Post.MaxLength = 100;
            tableLayout.Controls.Add(textBox_Post, 1, 5);

            Label label_HireDate = new Label();
            SetLabel(ref label_HireDate, "Дата найма");
            tableLayout.Controls.Add(label_HireDate, 0, 6);
            dateTimePicker_HireDate = new DateTimePicker();
            dateTimePicker_HireDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_HireDate, 1, 6);
            dateTimePicker_HireDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_HireDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

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
            //if (!int.TryParse(textBox_UserId.Text, out int tp_UserId)) { MessageBox.Show("Поле 'ID Пользователя' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_FullName.Text)) { MessageBox.Show("Поле 'ФИО' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_ManagerProfiles.Create(
                    new DBT_ManagerProfiles()
                    {
                        UserId = int.Parse(textBox_UserId.Text),
                        FullName = textBox_FullName.Text,
                        Phone = textBox_Phone.Text,
                        LicenseNumber = textBox_LicenseNumber.Text,
                        Email = textBox_Email.Text,
                        Post = textBox_Post.Text,
                        HireDate = dateTimePicker_HireDate.Value
                    }
                );
            }
            else
            {
                res = DBT_ManagerProfiles.Edit(
                    new DBT_ManagerProfiles()
                    {
                        ManagerId = Object.ManagerId,
                        UserId = Object.UserId,
                        FullName = textBox_FullName.Text,
                        Phone = textBox_Phone.Text,
                        LicenseNumber = textBox_LicenseNumber.Text,
                        Email = textBox_Email.Text,
                        Post = textBox_Post.Text,
                        HireDate = dateTimePicker_HireDate.Value
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
