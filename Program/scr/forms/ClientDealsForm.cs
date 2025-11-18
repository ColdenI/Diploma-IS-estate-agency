using Microsoft.Data.SqlClient;
using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class ClientDealsForm : Form
    {
        public ClientDealsForm(DBT_ClientProfiles clientProfiles)
        {
            InitializeComponent();

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

            dataGridView.Columns.Add("DealId", "№");
            dataGridView.Columns.Add("PropertyId", "Недвижимость");
            dataGridView.Columns.Add("ClientId", "Клиент"); dataGridView.Columns[2].Visible = false;
            dataGridView.Columns.Add("SalePrice", "Сумма");
            dataGridView.Columns.Add("Status", "Статус");
            dataGridView.Columns.Add("SignedDate", "Дата");
            dataGridView.Columns.Add("CommissionRate", "Комиссия %");
            dataGridView.Columns.Add("CreatedAt", "Дата создания");

            using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
            {
                connection.Open();
                using (var query = connection.CreateCommand())
                {
                    query.CommandText = $"SELECT * FROM Deals WHERE ClientId = {id}";
                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var index = dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = reader.GetInt32(0);
                            if (reader.IsDBNull(1)) dataGridView.Rows[index].Cells[1].Value = "-";
                            else
                            {
                                var property = DBT_Properties.GetById(reader.GetInt32(1));
                                dataGridView.Rows[index].Cells[1].Value = $"{property.Type} - [{property.Address}]";
                            }
                            dataGridView.Rows[index].Cells[2].Value = DBT_ClientProfiles.GetById(reader.GetInt32(2)).FullName;
                            dataGridView.Rows[index].Cells[3].Value = reader.GetDecimal(3);
                            dataGridView.Rows[index].Cells[4].Value = reader.GetString(4);
                            if (reader.IsDBNull(5)) dataGridView.Rows[index].Cells[5].Value = "-";
                            else dataGridView.Rows[index].Cells[5].Value = DateTime.Parse(reader.GetValue(5).ToString());
                            if (reader.IsDBNull(6)) dataGridView.Rows[index].Cells[6].Value = "-";
                            else dataGridView.Rows[index].Cells[6].Value = reader.GetDecimal(6);
                            if (reader.IsDBNull(7)) dataGridView.Rows[index].Cells[7].Value = "-";
                            else dataGridView.Rows[index].Cells[7].Value = DateTime.Parse(reader.GetValue(7).ToString());

                            
                        }
                    }
                }
            }
        }
    }
}
