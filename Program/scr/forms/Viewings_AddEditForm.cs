using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class Viewings_AddEditForm : Form
    {
        DBT_Viewings Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_RequestId;
        ComboBox comboBox_PropertyId;
        DateTimePicker dateTimePicker_ScheduledTime;
        TextBox textBox_Status;
        TextBox textBox_Notes;

        List<DBT_Properties> properties = new List<DBT_Properties>();
        List<DBT_Requests> requests = new List<DBT_Requests>();

        public Viewings_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Viewings_AddEditForm(DBT_Viewings obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            comboBox_RequestId.SelectedIndex = indexOf_requests(obj.RequestId);
            comboBox_PropertyId.SelectedIndex = indexOf_properties(obj.PropertyId);
            dateTimePicker_ScheduledTime.Value = (DateTime)((obj.ScheduledTime == null) ? DateTime.Now : obj.ScheduledTime);
            textBox_Status.Text = obj.Status.ToString();
            textBox_Notes.Text = obj.Notes.ToString();
        }

        private void Init()
        {
            this.Size = new Size(1200, 650);
            this.Text = "Показы - " + (Object == null ? "Добавить" : "Изменить").ToString();
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

            Label label_RequestId = new Label();
            SetLabel(ref label_RequestId, "Заявка");
            tableLayout.Controls.Add(label_RequestId, 0, 0);
            comboBox_RequestId = new ComboBox();
            comboBox_RequestId.Dock = DockStyle.Fill;
            comboBox_RequestId.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_RequestId, 1, 0);
            LoadComboBox_requests();

            Label label_PropertyId = new Label();
            SetLabel(ref label_PropertyId, "Недвижимость");
            tableLayout.Controls.Add(label_PropertyId, 0, 1);
            comboBox_PropertyId = new ComboBox();
            comboBox_PropertyId.Dock = DockStyle.Fill;
            comboBox_PropertyId.MaxLength = 1000;
            tableLayout.Controls.Add(comboBox_PropertyId, 1, 1);
            LoadComboBox_properties();

            Label label_ScheduledTime = new Label();
            SetLabel(ref label_ScheduledTime, "Дата");
            tableLayout.Controls.Add(label_ScheduledTime, 0, 2);
            dateTimePicker_ScheduledTime = new DateTimePicker();
            dateTimePicker_ScheduledTime.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_ScheduledTime, 1, 2);
            dateTimePicker_ScheduledTime.Format = DateTimePickerFormat.Custom;
            dateTimePicker_ScheduledTime.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_Status = new Label();
            SetLabel(ref label_Status, "Статус");
            tableLayout.Controls.Add(label_Status, 0, 3);
            textBox_Status = new TextBox();
            textBox_Status.Dock = DockStyle.Fill;
            textBox_Status.MaxLength = 30;
            tableLayout.Controls.Add(textBox_Status, 1, 3);

            Label label_Notes = new Label();
            SetLabel(ref label_Notes, "Описание");
            tableLayout.Controls.Add(label_Notes, 0, 4);
            textBox_Notes = new TextBox();
            textBox_Notes.Dock = DockStyle.Fill;
            textBox_Notes.MaxLength = 500;
            tableLayout.Controls.Add(textBox_Notes, 1, 4);

            this.Controls.Add(tableLayout);
        }

        private void LoadComboBox_requests()
        {
            requests = DBT_Requests.GetAll();

            comboBox_RequestId.Items.Clear();
            foreach (var i in requests)
                comboBox_RequestId.Items.Add($"№{i.RequestId}");
        }
        private int indexOf_requests(int id)
        {
            for (int i = 0; i < requests.Count; i++)
            {
                if (requests[i].RequestId == id) return i;
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
            if (comboBox_RequestId.SelectedIndex == -1) { MessageBox.Show("Поле 'Заявка' имеет некорректное значение!"); return; }
            if (comboBox_PropertyId.SelectedIndex == -1) { MessageBox.Show("Поле 'Недвижимость' имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Status.Text)) { MessageBox.Show("Поле 'Статус' имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Viewings.Create(
                    new DBT_Viewings()
                    {
                        RequestId = requests[comboBox_RequestId.SelectedIndex].RequestId,
                        PropertyId = properties[comboBox_PropertyId.SelectedIndex].PropertyId,
                        ScheduledTime = dateTimePicker_ScheduledTime.Value,
                        Status = textBox_Status.Text,
                        Notes = textBox_Notes.Text,
                    }
                );
            }
            else
            {
                res = DBT_Viewings.Edit(
                    new DBT_Viewings()
                    {
                        ViewingId = Object.ViewingId,
                        RequestId = requests[comboBox_RequestId.SelectedIndex].RequestId,
                        PropertyId = properties[comboBox_PropertyId.SelectedIndex].PropertyId,
                        ScheduledTime = dateTimePicker_ScheduledTime.Value,
                        Status = textBox_Status.Text,
                        Notes = textBox_Notes.Text,
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
