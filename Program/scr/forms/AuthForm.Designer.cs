namespace Program
{
    partial class AuthForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBox_login = new TextBox();
            textBox_password = new TextBox();
            button_login = new Button();
            button_reg = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Historic", 11.7818184F);
            label1.Location = new Point(97, 21);
            label1.Name = "label1";
            label1.Size = new Size(62, 25);
            label1.TabIndex = 1;
            label1.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Historic", 11.7818184F);
            label2.Location = new Point(97, 101);
            label2.Name = "label2";
            label2.Size = new Size(74, 25);
            label2.TabIndex = 2;
            label2.Text = "Пароль";
            // 
            // textBox_login
            // 
            textBox_login.Font = new Font("Segoe UI Historic", 11.7818184F);
            textBox_login.Location = new Point(15, 49);
            textBox_login.Name = "textBox_login";
            textBox_login.Size = new Size(229, 31);
            textBox_login.TabIndex = 3;
            textBox_login.Text = "admin1";
            // 
            // textBox_password
            // 
            textBox_password.Font = new Font("Segoe UI Historic", 11.7818184F);
            textBox_password.Location = new Point(15, 129);
            textBox_password.Name = "textBox_password";
            textBox_password.PasswordChar = '*';
            textBox_password.Size = new Size(229, 31);
            textBox_password.TabIndex = 4;
            textBox_password.Text = "pass123";
            // 
            // button_login
            // 
            button_login.Font = new Font("Segoe UI Historic", 11.7818184F);
            button_login.Location = new Point(15, 166);
            button_login.Name = "button_login";
            button_login.Size = new Size(229, 51);
            button_login.TabIndex = 5;
            button_login.Text = "Войти";
            button_login.UseVisualStyleBackColor = true;
            button_login.Click += button_login_Click;
            // 
            // button_reg
            // 
            button_reg.Font = new Font("Segoe UI Historic", 11.7818184F);
            button_reg.Location = new Point(15, 223);
            button_reg.Name = "button_reg";
            button_reg.Size = new Size(229, 32);
            button_reg.TabIndex = 6;
            button_reg.Text = "Зарегистрироваться";
            button_reg.UseVisualStyleBackColor = true;
            button_reg.Click += button_reg_Click;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(264, 268);
            Controls.Add(button_reg);
            Controls.Add(button_login);
            Controls.Add(textBox_password);
            Controls.Add(textBox_login);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Автроизация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox_login;
        private TextBox textBox_password;
        private Button button_login;
        private Button button_reg;
    }
}
