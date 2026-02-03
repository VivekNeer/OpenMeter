namespace OpenMeter
{
    partial class SettingsForm
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
            groupBox1 = new GroupBox();
            radioButtonKB = new RadioButton();
            radioButtonMB = new RadioButton();
            buttonOK = new Button();
            buttonCancel = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonKB);
            groupBox1.Controls.Add(radioButtonMB);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(260, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Speed Unit";
            // 
            // radioButtonKB
            // 
            radioButtonKB.AutoSize = true;
            radioButtonKB.Location = new Point(20, 60);
            radioButtonKB.Name = "radioButtonKB";
            radioButtonKB.Size = new Size(111, 24);
            radioButtonKB.TabIndex = 1;
            radioButtonKB.Text = "KB/s (Kilobytes)";
            radioButtonKB.UseVisualStyleBackColor = true;
            // 
            // radioButtonMB
            // 
            radioButtonMB.AutoSize = true;
            radioButtonMB.Checked = true;
            radioButtonMB.Location = new Point(20, 30);
            radioButtonMB.Name = "radioButtonMB";
            radioButtonMB.Size = new Size(126, 24);
            radioButtonMB.TabIndex = 0;
            radioButtonMB.TabStop = true;
            radioButtonMB.Text = "MB/s (Megabytes)";
            radioButtonMB.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(100, 130);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(80, 30);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += ButtonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(190, 130);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(80, 30);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 181);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Network Monitor Settings";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton radioButtonKB;
        private RadioButton radioButtonMB;
        private Button buttonOK;
        private Button buttonCancel;
    }
}
