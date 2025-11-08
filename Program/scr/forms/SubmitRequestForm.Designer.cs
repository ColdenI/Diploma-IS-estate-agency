namespace Program.scr.forms
{
    partial class SubmitRequestForm
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
            button = new Button();
            label1 = new Label();
            textBox_DesiredType = new TextBox();
            textBox_BudgetMin = new TextBox();
            textBox_BudgetMax = new TextBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button
            // 
            button.Location = new Point(388, 108);
            button.Name = "button";
            button.Size = new Size(150, 30);
            button.TabIndex = 2;
            button.Text = "Оставить заявку";
            button.UseVisualStyleBackColor = true;
            button.Click += button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Emoji", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(117, 20);
            label1.TabIndex = 3;
            label1.Text = "Желаемый тип ";
            // 
            // textBox_DesiredType
            // 
            textBox_DesiredType.Location = new Point(166, 12);
            textBox_DesiredType.MaxLength = 50;
            textBox_DesiredType.Name = "textBox_DesiredType";
            textBox_DesiredType.Size = new Size(372, 26);
            textBox_DesiredType.TabIndex = 4;
            // 
            // textBox_BudgetMin
            // 
            textBox_BudgetMin.Location = new Point(166, 44);
            textBox_BudgetMin.Name = "textBox_BudgetMin";
            textBox_BudgetMin.Size = new Size(372, 26);
            textBox_BudgetMin.TabIndex = 5;
            // 
            // textBox_BudgetMax
            // 
            textBox_BudgetMax.Location = new Point(166, 76);
            textBox_BudgetMax.Name = "textBox_BudgetMax";
            textBox_BudgetMax.Size = new Size(372, 26);
            textBox_BudgetMax.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Emoji", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 46);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 7;
            label2.Text = "Бюджет от";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Emoji", 9.818182F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 78);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 8;
            label3.Text = "Бюджет до";
            // 
            // SubmitRequestForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 150);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox_BudgetMax);
            Controls.Add(textBox_BudgetMin);
            Controls.Add(textBox_DesiredType);
            Controls.Add(label1);
            Controls.Add(button);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SubmitRequestForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Оставить заявку";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button;
        private Label label1;
        private TextBox textBox_DesiredType;
        private TextBox textBox_BudgetMin;
        private TextBox textBox_BudgetMax;
        private Label label2;
        private Label label3;
    }
}