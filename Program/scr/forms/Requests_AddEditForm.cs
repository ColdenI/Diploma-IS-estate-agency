using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Requests_AddEditForm : Form
    {
        DBT_Requests Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_ClientId;
        TextBox textBox_DesiredType;
        TextBox textBox_BudgetMin;
        TextBox textBox_BudgetMax;
        TextBox textBox_Status;

        List<DBT_ClientProfiles> clients = new List<DBT_ClientProfiles>();

        public Requests_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Requests_AddEditForm(DBT_Requests obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            comboBox_ClientId.SelectedIndex = indexOf_clients(obj.ClientId);
            textBox_DesiredType.Text = obj.DesiredType.ToString();
            textBox_BudgetMin.Text = obj.BudgetMin.ToString();
            textBox_BudgetMax.Text = obj.BudgetMax.ToString();
            textBox_Status.Text = obj.Status.ToString();
        }

        private void Init()
        {
            this.Size = new Size(1200, 650);
            this.Text = "Заявки - " + (Object == null ? "Добавить" : "Изменить").ToString();
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
                RowCount = 6
            };

            Label label_ClientId = new Label();
            SetLabel(ref label_ClientId, "Клиент");
            tableLayout.Controls.Add(label_ClientId, 0, 0);
            comboBox_ClientId = new ComboBox();
            comboBox_ClientId.Dock = DockStyle.Fill;
            comboBox_ClientId.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_ClientId, 1, 0);
            LoadComboBox_clients();

            Label label_DesiredType = new Label();
            SetLabel(ref label_DesiredType, "Желаемый тип");
            tableLayout.Controls.Add(label_DesiredType, 0, 1);
            textBox_DesiredType = new TextBox();
            textBox_DesiredType.Dock = DockStyle.Fill;
            textBox_DesiredType.MaxLength = 50;
            tableLayout.Controls.Add(textBox_DesiredType, 1, 1);

            Label label_BudgetMin = new Label();
            SetLabel(ref label_BudgetMin, "Бюджет от");
            tableLayout.Controls.Add(label_BudgetMin, 0, 2);
            textBox_BudgetMin = new TextBox();
            textBox_BudgetMin.Dock = DockStyle.Fill;
            textBox_BudgetMin.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_BudgetMin, 1, 2);

            Label label_BudgetMax = new Label();
            SetLabel(ref label_BudgetMax, "Бюджет до");
            tableLayout.Controls.Add(label_BudgetMax, 0, 3);
            textBox_BudgetMax = new TextBox();
            textBox_BudgetMax.Dock = DockStyle.Fill;
            textBox_BudgetMax.MaxLength = 1000;
            tableLayout.Controls.Add(textBox_BudgetMax, 1, 3);

            Label label_Status = new Label();
            SetLabel(ref label_Status, "Статус");
            tableLayout.Controls.Add(label_Status, 0, 4);
            textBox_Status = new TextBox();
            textBox_Status.Dock = DockStyle.Fill;
            textBox_Status.MaxLength = 30;
            tableLayout.Controls.Add(textBox_Status, 1, 4);

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
            if (comboBox_ClientId.SelectedIndex == -1) { MessageBox.Show("Поле 'Клиент' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_DesiredType.Text)) { MessageBox.Show("Поле 'Желаемый тип' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_BudgetMin.Text, out decimal tp_BudgetMin)) { MessageBox.Show("Поле 'Бюджет от' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_BudgetMax.Text, out decimal tp_BudgetMax)) { MessageBox.Show("Поле 'Бюджет до' имеет некорректное значение!"); return; }
            if (tp_BudgetMax < tp_BudgetMin) { MessageBox.Show("Поле 'Бюджет до' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Status.Text)) { MessageBox.Show("Поле 'Статус' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Requests.Create(
                    new DBT_Requests()
                    {
                        ClientId = clients[comboBox_ClientId.SelectedIndex].ClientId,
                        DesiredType = textBox_DesiredType.Text,
                        BudgetMin = decimal.Parse(textBox_BudgetMin.Text),
                        BudgetMax = decimal.Parse(textBox_BudgetMax.Text),
                        Status = textBox_Status.Text
                    }
                );
            }
            else
            {
                res = DBT_Requests.Edit(
                    new DBT_Requests()
                    {
                        RequestId = Object.RequestId,
                        ClientId = clients[comboBox_ClientId.SelectedIndex].ClientId,
                        DesiredType = textBox_DesiredType.Text,
                        BudgetMin = decimal.Parse(textBox_BudgetMin.Text),
                        BudgetMax = decimal.Parse(textBox_BudgetMax.Text),
                        Status = textBox_Status.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
