using Microsoft.Data.SqlClient;
using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms.userControls
{
    public partial class Properties_ViewUserControl : UserControl
    {
        DataGridView dataGridView;
        TextBox textBox_search;
        Button button_edit;
        Button button_update;
        Button button_create;
        Button button_photos;
        Button button_remove;

        public Properties_ViewUserControl()
        {
            InitializeComponent();

            this.Load += Properties_ViewForm_Load;
            this.Disposed += Properties_ViewForm_Disposed;
            this.Size = new Size(1200, 650);
            this.Text = "Недвижимость";

            button_create = new Button()
            {
                Text = "Добавить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_photos = new Button()
            {
                Text = "Фотографии",
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
            button_photos.Click += Button_photos_Click;
            button_remove.Click += Button_remove_Click;
            button_create.Click += Button_create_Click;
            button_edit.Click += Button_edit_Click;
            textBox_search.TextChanged += TextBox_search_TextChanged;
            dataGridView.DataError += (s, e) => { };

            this.Controls.Add(button_create);
            this.Controls.Add(button_photos);
            this.Controls.Add(button_edit);
            this.Controls.Add(button_remove);
            this.Controls.Add(button_update);
            this.Controls.Add(textBox_search);
            this.Controls.Add(dataGridView);

            if(Core.ThisUser.Role == "Client")
            {
                button_create.Visible = false;
                button_edit.Visible = false;
                button_remove.Visible = false;
            }
        }

        private void Button_photos_Click(object? sender, EventArgs e)
        {
            new PropertyPhotosForm((int)dataGridView.CurrentCell.OwningRow.Cells[0].Value).ShowDialog();
            UpdateTable();
        }

        private void Properties_ViewForm_Disposed(object? sender, EventArgs e)
        {
            this.Load -= Properties_ViewForm_Load;
            this.Disposed -= Properties_ViewForm_Disposed;
            button_update.Click -= Button_update_Click;
            button_photos.Click -= Button_photos_Click;
            button_remove.Click -= Button_remove_Click;
            button_create.Click -= Button_create_Click;
            button_edit.Click -= Button_edit_Click;
            textBox_search.TextChanged -= TextBox_search_TextChanged;
        }

        private void Button_edit_Click(object? sender, EventArgs e)
        {
            new Properties_AddEditForm(DBT_Properties.GetById((int)dataGridView.CurrentCell.OwningRow.Cells[0].Value)).ShowDialog();
            UpdateTable();
        }

        private void Button_create_Click(object? sender, EventArgs e)
        {
            new Properties_AddEditForm().ShowDialog();
            UpdateTable();
        }

        private void Button_remove_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить запись?\r\nОтменить будет невозможно!\r\n", "Удалить", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            int res = DBT_Properties.Remove((int)dataGridView.CurrentCell.OwningRow.Cells[0].Value);

            if (res == -1) MessageBox.Show("Ошибка удаления! Объект используеться!");
            else if (res == 0) MessageBox.Show("Успешно удалено!");
            UpdateTable();
        }

        private void TextBox_search_TextChanged(object? sender, EventArgs e) => UpdateTable();
        private void Button_update_Click(object? sender, EventArgs e) => UpdateTable();
        private void Properties_ViewForm_Load(object sender, EventArgs e) => UpdateTable();

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

            dataGridView.Columns.Add("PropertyId", "ID"); dataGridView.Columns[0].Visible = false;

            //dataGridView.Columns.Add("Image", "Фото"); dataGridView.Columns[1].Width = 100;
            var imageColumn = new DataGridViewImageColumn
            {
                Name = "Image",
                HeaderText = "Фото",
                Width = 100,
                ImageLayout = DataGridViewImageCellLayout.Zoom, // Уменьшает/увеличивает изображение под размер ячейки
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            dataGridView.Columns.Add(imageColumn);

            dataGridView.Columns.Add("ManagerId", "Менеджер");
            dataGridView.Columns.Add("Address", "Адрес");
            dataGridView.Columns.Add("Type", "Тип");
            dataGridView.Columns.Add("Description", "Описание");
            dataGridView.Columns.Add("Area", "Площадь");
            dataGridView.Columns.Add("Rooms", "Количество комнат");
            dataGridView.Columns.Add("Price", "Цена");
            dataGridView.Columns.Add("Status", "Статус");
            dataGridView.Columns.Add("CreatedAt", "Дата создания");
            dataGridView.Columns.Add("UpdatedAt", "Дата изменения");

            using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
            {
                connection.Open();
                using (var query = connection.CreateCommand())
                {
                    query.CommandText = "SELECT * FROM Properties";
                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var index = dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = reader.GetInt32(0);
                            if (reader.IsDBNull(1)) dataGridView.Rows[index].Cells[2].Value = "-";
                            else dataGridView.Rows[index].Cells[2].Value = DBT_ManagerProfiles.GetById(reader.GetInt32(1)).FullName;
                            dataGridView.Rows[index].Cells[3].Value = reader.GetString(2);
                            dataGridView.Rows[index].Cells[4].Value = reader.GetString(3);
                            if (reader.IsDBNull(4)) dataGridView.Rows[index].Cells[5].Value = "-";
                            else dataGridView.Rows[index].Cells[5].Value = reader.GetString(4);
                            dataGridView.Rows[index].Cells[6].Value = reader.GetDecimal(5);
                            dataGridView.Rows[index].Cells[7].Value = reader.GetInt32(6);
                            dataGridView.Rows[index].Cells[8].Value = reader.GetDecimal(7);
                            dataGridView.Rows[index].Cells[9].Value = reader.GetString(8);
                            if (reader.IsDBNull(9)) dataGridView.Rows[index].Cells[10].Value = "-";
                            else dataGridView.Rows[index].Cells[10].Value = DateTime.Parse(reader.GetValue(9).ToString());
                            if (reader.IsDBNull(10)) dataGridView.Rows[index].Cells[11].Value = "-";
                            else dataGridView.Rows[index].Cells[11].Value = DateTime.Parse(reader.GetValue(10).ToString());

                            // Загружаем изображение (если есть)
                            Image img = null;
                            var photo = DBT_Photos.GetAllByPropertyId(reader.GetInt32(0));
                            if (photo != null)
                            {
                                var _p = photo[0];
                                foreach (var i in photo) if (i.IsMain != null) if ((bool)i.IsMain) _p = i;
                                string base64 = _p.PhotoData;
                                img = Core.Base64ToImage(base64);
                            }
                            dataGridView.Rows[index].Cells[1].Value = img;

                            string search = textBox_search.Text.ToLower();
                            if (!string.IsNullOrWhiteSpace(search))
                                if (
                                    !dataGridView.Rows[index].Cells[2].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[3].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[4].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[5].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[6].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[7].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[8].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[9].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[10].Value.ToString().ToLower().Contains(search) &&
                                    !dataGridView.Rows[index].Cells[11].Value.ToString().ToLower().Contains(search)
                                ) dataGridView.Rows.RemoveAt(index);
                        }
                    }
                }
            }
        }
    }


}
