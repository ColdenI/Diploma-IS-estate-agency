using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class ClientForm : Form
    {
        private DBT_ClientProfiles Client;

        public ClientForm()
        {
            InitializeComponent();
            var control = new userControls.Properties_ViewUserControl();
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
            Client = DBT_ClientProfiles.GetByUserId(Core.ThisUser.UserId);
            label1.Text = $"Добро пожаловать, {Client.FullName}";
        }

        private void button_Click(object sender, EventArgs e) => new SubmitRequestForm(Client).ShowDialog();

        private void button2_Click(object sender, EventArgs e)
        {
            new ClientRequestsForm(Client).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ClientDealsForm(Client).ShowDialog();
        }
    }
}
