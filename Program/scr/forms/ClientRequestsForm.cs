using Microsoft.Data.SqlClient;
using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class ClientRequestsForm : Form
    {
        public ClientRequestsForm(DBT_ClientProfiles clientProfiles)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            UpdateTable(clientProfiles.ClientId);
        }

        private void UpdateTable(int id)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.BringToFront();
            dataGridView.ReadOnly = true;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView.Columns.Add("RequestId", "№");
            dataGridView.Columns.Add("ClientId", "Клиент"); dataGridView.Columns[1].Visible = false;
            dataGridView.Columns.Add("DesiredType", "Желаемый тип");
            dataGridView.Columns.Add("BudgetMin", "Бюджет от");
            dataGridView.Columns.Add("BudgetMax", "Бюджет до");
            dataGridView.Columns.Add("Status", "Статус");
            dataGridView.Columns.Add("CreatedAt", "Дата создания");

            using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
            {
                connection.Open();
                using (var query = connection.CreateCommand())
                {
                    query.CommandText = $"SELECT * FROM Requests WHERE ClientId = {id}";
                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var index = dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = reader.GetInt32(0);
                            dataGridView.Rows[index].Cells[1].Value = DBT_ClientProfiles.GetById(reader.GetInt32(1)).FullName;
                            dataGridView.Rows[index].Cells[2].Value = reader.GetString(2);
                            if (reader.IsDBNull(3)) dataGridView.Rows[index].Cells[3].Value = "-";
                            else dataGridView.Rows[index].Cells[3].Value = reader.GetDecimal(3);
                            if (reader.IsDBNull(4)) dataGridView.Rows[index].Cells[4].Value = "-";
                            else dataGridView.Rows[index].Cells[4].Value = reader.GetDecimal(4);
                            dataGridView.Rows[index].Cells[5].Value = reader.GetString(5);
                            if (reader.IsDBNull(6)) dataGridView.Rows[index].Cells[6].Value = "-";
                            else dataGridView.Rows[index].Cells[6].Value = DateTime.Parse(reader.GetValue(6).ToString());                         
                        }
                    }
                }
            }
        }
    }
}
