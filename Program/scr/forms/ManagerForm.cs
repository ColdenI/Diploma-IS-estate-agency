using Program.scr.core;
using Program.scr.core.dbt;

namespace Program.scr.forms
{
    public partial class ManagerForm : Form
    {
        private UserControl UControl;

        public ManagerForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            var manager = DBT_ManagerProfiles.GetByUserId(Core.ThisUser.UserId);
            this.Text = $"Менеджер - [{manager.FullName}] - {Core.ThisUser.Role}";
            this.StartPosition = FormStartPosition.CenterScreen;

            if(Core.ThisUser.Role == "Manager")
            {
                toolStripButton4.Visible = false;
                toolStripButton5.Visible = false;
            }

            ShowControl(new userControls.Properties_ViewUserControl());
        }

        private void ShowControl(UserControl control)
        {
            if (UControl != null)
            {
                panel1.Controls.Remove(UControl);
                UControl = null;
            }

            UControl = control;
            UControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(UControl);
        }

        private void toolStripButton1_Click(object sender, EventArgs e) => ShowControl(new userControls.Deals_ViewUserControl());
        private void toolStripButton5_Click(object sender, EventArgs e) => ShowControl(new userControls.Users_ViewUserControl());
        private void toolStripButton4_Click(object sender, EventArgs e) => ShowControl(new userControls.ManagerProfiles_ViewUserControl());
        private void toolStripButton3_Click(object sender, EventArgs e) => ShowControl(new userControls.ClientProfiles_ViewUserControl());
        private void toolStripButton6_Click(object sender, EventArgs e) => ShowControl(new userControls.Viewings_ViewUserControl());
        private void toolStripButton2_Click(object sender, EventArgs e) => ShowControl(new userControls.Properties_ViewUserControl());
        private void toolStripButton7_Click(object sender, EventArgs e) => ShowControl(new userControls.Requests_ViewUserControl());
    }
}
