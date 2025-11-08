using Microsoft.Data.SqlClient;
using Program.scr.core;
using Program.scr.core.dbt;
using System.Windows.Forms;

namespace Program.scr.forms.userControls
{
    public partial class ManagerProfiles_ViewUserControl : UserControl
    {
        DataGridView dataGridView;
        TextBox textBox_search;
        Button button_edit;
        Button button_update;
        Button button_create;
        Button button_remove;

        public ManagerProfiles_ViewUserControl()
        {
            InitializeComponent();

            this.Load += ManagerProfiles_ViewForm_Load;
            this.Disposed += ManagerProfiles_ViewForm_Disposed;
            this.Size = new Size(1200, 650);
            this.Text = "Менеджеры";

            button_create = new Button()
            {
                Text = "Добавить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_edit = new Button()
            {
                Text = "Изменить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_remove = new Button()
            {
                Text = "Удалить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_update = new Button()
            {
                Text = "Обновить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            textBox_search = new TextBox()
            {
                Dock = DockStyle.Top
            };
            dataGridView = new DataGridView()
            {
                Dock = DockStyle.Fill
            };

            button_update.Click += Button_update_Click;
            button_remove.Click += Button_remove_Click;
            button_create.Click += Button_create_Click;
            button_edit.Click += Button_edit_Click;
            textBox_search.TextChanged += TextBox_search_TextChanged;

            this.Controls.Add(button_create);
            this.Controls.Add(button_edit);
            this.Controls.Add(button_remove);
            this.Controls.Add(button_update);
            this.Controls.Add(textBox_search);
            this.Controls.Add(dataGridView);
        }

        private void ManagerProfiles_ViewForm_Disposed(object? sender, EventArgs e)
        {
            this.Load -= ManagerProfiles_ViewForm_Load;
            this.Disposed -= ManagerProfiles_ViewForm_Disposed;
            button_update.Click -= Button_update_Click;
            button_remove.Click -= Button_remove_Click;
            button_create.Click -= Button_create_Click;
            button_edit.Click -= Button_edit_Click;
            textBox_search.TextChanged -= TextBox_search_TextChanged;
        }

        private void Button_edit_Click(object? sender, EventArgs e)
        {
            new ManagerProfiles_AddEditForm(DBT_ManagerProfiles.GetById((int)dataGridView.CurrentCell.OwningRow.Cells[0].Value)).ShowDialog();
            UpdateTable();
        }

        private void Button_create_Click(object? sender, EventArgs e)
        {
            new RegistrationEmployeeForm().ShowDialog();
            UpdateTable();
        }

        private void Button_remove_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить запись?\r\nОтменить будет невозможно!\r\n", "Удалить", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            var select = DBT_ManagerProfiles.GetById((int)dataGridView.CurrentCell.OwningRow.Cells[0].Value);
            int res = DBT_ManagerProfiles.Remove(select.ManagerId);
            res = DBT_Users.Remove(select.UserId);
            if (res <= -1) MessageBox.Show("Ошибка удаления! Объект используеться!");
            else if (res >= 0) MessageBox.Show("Успешно удалено!");
            UpdateTable();
        }

        private void TextBox_search_TextChanged(object? sender, EventArgs e) => UpdateTable();
        private void Button_update_Click(object? sender, EventArgs e) => UpdateTable();
        private void ManagerProfiles_ViewForm_Load(object sender, EventArgs e) => UpdateTable();

        private void UpdateTable()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.BringToFront();
            dataGridView.ReadOnly = true;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView.Columns.Add("ManagerId", "ID"); dataGridView.Columns[0].Visible = false;
            dataGridView.Columns.Add("UserId", "Логин");
            dataGridView.Columns.Add("FullName", "ФИО");
            dataGridView.Columns.Add("Phone", "Номер телефона");
            dataGridView.Columns.Add("LicenseNumber", "Лицензия");
            dataGridView.Columns.Add("Email", "Электронная почта");
            dataGridView.Columns.Add("Post", "Должность");
            dataGridView.Columns.Add("HireDate", "Дата найма");

            using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
            {
                connection.Open();
                using (var query = connection.CreateCommand())
                {
                    query.CommandText = "SELECT * FROM ManagerProfiles";
                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var index = dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = reader.GetInt32(0);
                            dataGridView.Rows[index].Cells[1].Value = DBT_Users.GetById(reader.GetInt32(1)).Login;
                            dataGridView.Rows[index].Cells[2].Value = reader.GetString(2);
                            if (reader.IsDBNull(3)) dataGridView.Rows[index].Cells[3].Value = "-";
                            else dataGridView.Rows[index].Cells[3].Value = reader.GetString(3);
                            if (reader.IsDBNull(4)) dataGridView.Rows[index].Cells[4].Value = "-";
                            else dataGridView.Rows[index].Cells[4].Value = reader.GetString(4);
                            if (reader.IsDBNull(5)) dataGridView.Rows[index].Cells[5].Value = "-";
                            else dataGridView.Rows[index].Cells[5].Value = reader.GetString(5);
                            if (reader.IsDBNull(6)) dataGridView.Rows[index].Cells[6].Value = "-";
                            else dataGridView.Rows[index].Cells[6].Value = reader.GetString(6);
                            if (reader.IsDBNull(7)) dataGridView.Rows[index].Cells[7].Value = "-";
                            else dataGridView.Rows[index].Cells[7].Value = DateTime.Parse(reader.GetValue(7).ToString());

                            string search = textBox_search.Text.ToLower();
                            if (!string.IsNullOrWhiteSpace(search))
                                if (
                                    !dataGridView.Rows[index].Cells[0].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[1].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[2].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[3].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[4].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[5].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[6].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[7].Value.ToString().ToLower().Contains(search)
                                ) dataGridView.Rows.RemoveAt(index);
                        }
                    }
                }
            }
        }
    }


}
