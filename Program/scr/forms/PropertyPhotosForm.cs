using Program.scr.core;
using Program.scr.core.dbt;
using System.Windows.Forms;

namespace Program.scr.forms
{
    public partial class PropertyPhotosForm : Form
    {
        private int PropertyId;
        private List<DBT_Photos> photos;

        public PropertyPhotosForm(int PropertyId)
        {
            InitializeComponent();
            this.PropertyId = PropertyId;
            photos = new List<DBT_Photos>();

            LoadPhotos();

            if(Core.ThisUser.Role == "Client")
            {
                button_remove.Visible = false;
                button_upload.Visible = false;
            }
        }

        private void LoadPhotos()
        {
            photos = new List<DBT_Photos>();
            photos.Clear();
            pictureBox.Image = null;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            listBox_photos.Items.Clear();
            photos = DBT_Photos.GetAllByPropertyId(this.PropertyId);
            if (photos == null) return;
            for (int i = 0; i < photos.Count; i++) listBox_photos.Items.Add($"{this.PropertyId}_{i}");
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            string base64 = Core.GetImageBase64FromFileDialog();
            if (string.IsNullOrEmpty(base64))
            {
                MessageBox.Show("Изображение не выбрано.");
                return;
            }

            int res = DBT_Photos.Create(new DBT_Photos()
            {
                PropertyId = this.PropertyId,
                PhotoData = base64,
                IsMain = (listBox_photos.Items.Count == 0)
            });

            if (res == -1)
            {
                MessageBox.Show("Изображение не выбрано.", "Ой...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadPhotos();
        }

        private void listBox_photos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_photos.SelectedIndex == -1) return;
            pictureBox.Image = Core.Base64ToImage(photos[listBox_photos.SelectedIndex].PhotoData);
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            if (listBox_photos.SelectedIndex == -1) return;
            DBT_Photos.Remove(photos[listBox_photos.SelectedIndex].PhotoId);          
            LoadPhotos();
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            if (listBox_photos.SelectedIndex == -1) return;
            Core.SaveImageFromBase64(photos[listBox_photos.SelectedIndex].PhotoData, listBox_photos.SelectedItem.ToString());
        }
    }
}
