using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Deals_AddEditForm : Form
    {
        DBT_Deals Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_PropertyId;
        ComboBox comboBox_ClientId;
        TextBox textBox_SalePrice;
        TextBox textBox_Status;
        DateTimePicker dateTimePicker_SignedDate;
        TextBox textBox_CommissionRate;

        List<DBT_Properties> properties = new List<DBT_Properties>();
        List<DBT_ClientProfiles> clients = new List<DBT_ClientProfiles>();

        public Deals_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Deals_AddEditForm(DBT_Deals obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            if (obj.PropertyId != null) comboBox_PropertyId.SelectedIndex = indexOf_properties((int)obj.PropertyId);
            comboBox_ClientId.SelectedIndex = indexOf_clients(obj.ClientId);
            textBox_SalePrice.Text = obj.SalePrice.ToString();
            textBox_Status.Text = obj.Status.ToString();
            dateTimePicker_SignedDate.Value = (DateTime)((obj.SignedDate == null) ? DateTime.Now : obj.SignedDate);
            textBox_CommissionRate.Text = obj.CommissionRate.ToString();
        }

        private void Init()
        {
            this.Size = new Size(1200, 650);
            this.Text = "Сделки - " + (Object == null ? "Добавить" : "Изменить").ToString();
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

            Label label_PropertyId = new Label();
            SetLabel(ref label_PropertyId, "Недвижимость");
            tableLayout.Controls.Add(label_PropertyId, 0, 0);
            comboBox_PropertyId = new ComboBox();
            comboBox_PropertyId.Dock = DockStyle.Fill;
            comboBox_PropertyId.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_PropertyId, 1, 0);
            LoadComboBox_properties();

            Label label_ClientId = new Label();
            SetLabel(ref label_ClientId, "Клиент");
            tableLayout.Controls.Add(label_ClientId, 0, 1);
            comboBox_ClientId = new ComboBox();
            comboBox_ClientId.Dock = DockStyle.Fill;
            comboBox_ClientId.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ClientId, 1, 1);
            LoadComboBox_clients();

            Label label_SalePrice = new Label();
            SetLabel(ref label_SalePrice, "Сумма");
            tableLayout.Controls.Add(label_SalePrice, 0, 2);
            textBox_SalePrice = new TextBox();
            textBox_SalePrice.Dock = DockStyle.Fill;
            textBox_SalePrice.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_SalePrice, 1, 2);

            Label label_Status = new Label();
            SetLabel(ref label_Status, "Статус");
            tableLayout.Controls.Add(label_Status, 0, 3);
            textBox_Status = new TextBox();
            textBox_Status.Dock = DockStyle.Fill;
            textBox_Status.MaxLength = 30;
            tableLayout.Controls.Add(textBox_Status, 1, 3);

            Label label_SignedDate = new Label();
            SetLabel(ref label_SignedDate, "Дата");
            tableLayout.Controls.Add(label_SignedDate, 0, 4);
            dateTimePicker_SignedDate = new DateTimePicker();
            dateTimePicker_SignedDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_SignedDate, 1, 4);
            dateTimePicker_SignedDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_SignedDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_CommissionRate = new Label();
            SetLabel(ref label_CommissionRate, "Комиссия (%)");
            tableLayout.Controls.Add(label_CommissionRate, 0, 5);
            textBox_CommissionRate = new TextBox();
            textBox_CommissionRate.Dock = DockStyle.Fill;
            textBox_CommissionRate.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_CommissionRate, 1, 5);

            this.Controls.Add(tableLayout);
        }

        private void LoadComboBox_clients()
        {
            clients = DBT_ClientProfiles.GetAll();

            comboBox_ClientId.Items.Clear();
            foreach (var i in clients)
                comboBox_ClientId.Items.Add(i.FullName);
        }
        private int indexOf_clients(int id)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ClientId == id) return i;
            }
            return -1;
        }
        private void LoadComboBox_properties()
        {
            properties = DBT_Properties.GetAll();

            comboBox_PropertyId.Items.Clear();
            foreach (var i in properties)
                comboBox_PropertyId.Items.Add($"{i.Type} - [{i.Address}]");
        }
        private int indexOf_properties(int id)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].PropertyId == id) return i;
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
            int? _property = null;
            if (comboBox_PropertyId.SelectedIndex != -1) _property = properties[comboBox_PropertyId.SelectedIndex].PropertyId;           
            if (comboBox_ClientId.SelectedIndex == -1) { MessageBox.Show("Поле 'Клиент' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_SalePrice.Text, out decimal tp_SalePrice)) { MessageBox.Show("Поле 'Сумма' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Status.Text)) { MessageBox.Show("Поле 'Статус' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_CommissionRate.Text, out decimal tp_CommissionRate)) { MessageBox.Show("Поле 'Комиссия' имеет некорректное значение!"); return; }
            if (tp_CommissionRate < 0 || tp_CommissionRate > 100) { MessageBox.Show("Поле 'Комиссия' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Deals.Create(
                    new DBT_Deals()
                    {
                        PropertyId = _property,
                        ClientId = clients[comboBox_ClientId.SelectedIndex].ClientId,
                        SalePrice = decimal.Parse(textBox_SalePrice.Text),
                        Status = textBox_Status.Text,
                        SignedDate = dateTimePicker_SignedDate.Value,
                        CommissionRate = decimal.Parse(textBox_CommissionRate.Text),
                    }
                );
            }
            else
            {
                res = DBT_Deals.Edit(
                    new DBT_Deals()
                    {
                        DealId = Object.DealId,
                        PropertyId = _property,
                        ClientId = clients[comboBox_ClientId.SelectedIndex].ClientId,
                        SalePrice = decimal.Parse(textBox_SalePrice.Text),
                        Status = textBox_Status.Text,
                        SignedDate = dateTimePicker_SignedDate.Value,
                        CommissionRate = decimal.Parse(textBox_CommissionRate.Text),
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
