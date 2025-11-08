using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class SubmitRequestForm : Form
    {
        private DBT_ClientProfiles Client;

        public SubmitRequestForm(DBT_ClientProfiles client)
        {
            InitializeComponent();
            Client = client;
            if (Client == null) Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_DesiredType.Text)) { MessageBox.Show("Поле 'Желаемый тип' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_BudgetMin.Text, out decimal tp_BudgetMin)) { MessageBox.Show("Поле 'Бюджет от' имеет некорректное значение!"); return; }
            if (!decimal.TryParse(textBox_BudgetMax.Text, out decimal tp_BudgetMax)) { MessageBox.Show("Поле 'Бюджет до' имеет некорректное значение!"); return; }
            if (tp_BudgetMax < tp_BudgetMin) { MessageBox.Show("Поле 'Бюджет до' имеет некорректное значение!"); return; }

            int res = DBT_Requests.Create(
                    new DBT_Requests()
                    {
                        ClientId = Client.ClientId,
                        DesiredType = textBox_DesiredType.Text,
                        BudgetMin = decimal.Parse(textBox_BudgetMin.Text),
                        BudgetMax = decimal.Parse(textBox_BudgetMax.Text),
                        Status = "Новый"
                    }
                );

            if(res == -1) 
            {
                MessageBox.Show("Ошибка!");
                return;
            }

            MessageBox.Show("Заявка успешно отправлена!");
            Close();
        }
    }
}
