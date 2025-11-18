namespace Program.scr.forms
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            panel1 = new Panel();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1109, 30);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(192, 192, 255);
            button1.Dock = DockStyle.Bottom;
            button1.Location = new Point(0, 570);
            button1.Name = "button1";
            button1.Size = new Size(1109, 35);
            button1.TabIndex = 1;
            button1.Text = "Оставить заявку";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button_Click;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(1109, 470);
            panel1.TabIndex = 2;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 255, 192);
            button2.Dock = DockStyle.Bottom;
            button2.Location = new Point(0, 535);
            button2.Name = "button2";
            button2.Size = new Size(1109, 35);
            button2.TabIndex = 3;
            button2.Text = "Мои заявки";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(192, 255, 255);
            button3.Dock = DockStyle.Bottom;
            button3.Location = new Point(0, 500);
            button3.Name = "button3";
            button3.Size = new Size(1109, 35);
            button3.TabIndex = 4;
            button3.Text = "Мои сделки";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1109, 605);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "ClientForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Агенство Недвижимости";
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button button1;
        private Panel panel1;
        private Button button2;
        private Button button3;
    }
}