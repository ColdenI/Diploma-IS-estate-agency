using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Properties_AddEditForm : Form
    {
        DBT_Properties Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_ManagerId;
        TextBox textBox_Address;
        TextBox textBox_Type;
        TextBox textBox_Description;
        TextBox textBox_Area;
        TextBox textBox_Rooms;
        TextBox textBox_Price;
        TextBox textBox_Status;

        List<DBT_ManagerProfiles> managers = new List<DBT_ManagerProfiles>();

        public Properties_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Properties_AddEditForm(DBT_Properties obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            if(obj.ManagerId != null) comboBox_ManagerId.SelectedIndex = indexOf_Manager((int)obj.ManagerId);
            textBox_Address.Text = obj.Address.ToString();
            textBox_Type.Text = obj.Type.ToString();
            textBox_Description.Text = obj.Description.ToString();
            textBox_Area.Text = obj.Area.ToString();
            textBox_Rooms.Text = obj.Rooms.ToString();
            textBox_Price.Text = obj.Price.ToString();
            textBox_Status.Text = obj.Status.ToString();
        }

        private void Init()
        {
            this.Size = new Size(1200, 650);
            this.Text = "Недвижимость - " + (Object == null ? "Добавить" : "Изменить").ToString();
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
                RowCount = 10
            };

            Label label_ManagerId = new Label();
            SetLabel(ref label_ManagerId, "Менеджер");
            tableLayout.Controls.Add(label_ManagerId, 0, 0);
            comboBox_ManagerId = new ComboBox();
            comboBox_ManagerId.Dock = DockStyle.Fill;
            comboBox_ManagerId.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ManagerId, 1, 0);
            LoadComboBox_Manager();

            Label label_Address = new Label();
            SetLabel(ref label_Address, "Адрес");
            tableLayout.Controls.Add(label_Address, 0, 1);
            textBox_Address = new TextBox();
            textBox_Address.Dock = DockStyle.Fill;
            textBox_Address.MaxLength = 255;
            tableLayout.Controls.Add(textBox_Address, 1, 1);

            Label label_Type = new Label();
            SetLabel(ref label_Type, "Тип");
            tableLayout.Controls.Add(label_Type, 0, 2);
            textBox_Type = new TextBox();
            textBox_Type.Dock = DockStyle.Fill;
            textBox_Type.MaxLength = 50;
            tableLayout.Controls.Add(textBox_Type, 1, 2);

            Label label_Description = new Label();
            SetLabel(ref label_Description, "Описание");
            tableLayout.Controls.Add(label_Description, 0, 3);
            textBox_Description = new TextBox();
            textBox_Description.Dock = DockStyle.Fill;
            textBox_Description.MaxLength = 500;
            tableLayout.Controls.Add(textBox_Description, 1, 3);

            Label label_Area = new Label();
            SetLabel(ref label_Area, "Площадь");
            tableLayout.Controls.Add(label_Area, 0, 4);
            textBox_Area = new TextBox();
            textBox_Area.Dock = DockStyle.Fill;
            textBox_Area.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Area, 1, 4);

            Label label_Rooms = new Label();
            SetLabel(ref label_Rooms, "Количество комнат");
            tableLayout.Controls.Add(label_Rooms, 0, 5);
            textBox_Rooms = new TextBox();
            textBox_Rooms.Dock = DockStyle.Fill;
            textBox_Rooms.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Rooms, 1, 5);

            Label label_Price = new Label();
            SetLabel(ref label_Price, "Цена");
            tableLayout.Controls.Add(label_Price, 0, 6);
            textBox_Price = new TextBox();
            textBox_Price.Dock = DockStyle.Fill;
            textBox_Price.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_Price, 1, 6);

            Label label_Status = new Label();
            SetLabel(ref label_Status, "Статус");
            tableLayout.Controls.Add(label_Status, 0, 7);
            textBox_Status = new TextBox();
            textBox_Status.Dock = DockStyle.Fill;
            textBox_Status.MaxLength = 30;
            tableLayout.Controls.Add(textBox_Status, 1, 7);

            this.Controls.Add(tableLayout);
        }

        private void LoadComboBox_Manager()
        {
            managers = DBT_ManagerProfiles.GetAll();

            comboBox_ManagerId.Items.Clear();
            foreach (var i in managers)
                comboBox_ManagerId.Items.Add(i.FullName);
        }
        private int indexOf_Manager(int id)
        {
            for (int i = 0; i < managers.Count; i++)
            {
                if (managers[i].ManagerId == id) return i;
            }
            return -1;
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
            int? manager_ = null;
            if(comboBox_ManagerId.SelectedIndex != -1) manager_ = managers[comboBox_ManagerId.SelectedIndex].ManagerId; 
            if (string.IsNullOrWhiteSpace(textBox_Address.Text)) { MessageBox.Show("Поле 'Адрес' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Type.Text)) { MessageBox.Show("Поле 'Тип' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_Area.Text, out decimal tp_Area)) { MessageBox.Show("Поле 'Площадь' имеет некорректное значение!"); return; }
            if (!int.TryParse(textBox_Rooms.Text, out int tp_Rooms)) { MessageBox.Show("Поле 'Количество комнат' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_Price.Text, out decimal tp_Price)) { MessageBox.Show("Поле 'Цена' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Status.Text)) { MessageBox.Show("Поле 'Статус' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Properties.Create(
                    new DBT_Properties()
                    {
                        ManagerId = manager_,
                        Address = textBox_Address.Text,
                        Type = textBox_Type.Text,
                        Description = textBox_Description.Text,
                        Area = decimal.Parse(textBox_Area.Text),
                        Rooms = int.Parse(textBox_Rooms.Text),
                        Price = decimal.Parse(textBox_Price.Text),
                        Status = textBox_Status.Text
                    }
                );
            }
            else
            {
                res = DBT_Properties.Edit(
                    new DBT_Properties()
                    {
                        PropertyId = Object.PropertyId,
                        ManagerId = manager_,
                        Address = textBox_Address.Text,
                        Type = textBox_Type.Text,
                        Description = textBox_Description.Text,
                        Area = decimal.Parse(textBox_Area.Text),
                        Rooms = int.Parse(textBox_Rooms.Text),
                        Price = decimal.Parse(textBox_Price.Text),
                        Status = textBox_Status.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
