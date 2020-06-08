namespace TravelAgency
{
    partial class FormAddGuides
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddGuides));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.comboBoxGuide = new System.Windows.Forms.ComboBox();
            this.labelCountGuide = new System.Windows.Forms.Label();
            this.labelGuideName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Location = new System.Drawing.Point(261, 63);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 28);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonSave.Location = new System.Drawing.Point(174, 63);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(72, 28);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(81, 39);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.ReadOnly = true;
            this.textBoxCount.Size = new System.Drawing.Size(253, 20);
            this.textBoxCount.TabIndex = 9;
            this.textBoxCount.Text = "1";
            // 
            // comboBoxGuide
            // 
            this.comboBoxGuide.FormattingEnabled = true;
            this.comboBoxGuide.Location = new System.Drawing.Point(81, 12);
            this.comboBoxGuide.Name = "comboBoxGuide";
            this.comboBoxGuide.Size = new System.Drawing.Size(253, 21);
            this.comboBoxGuide.TabIndex = 8;
            // 
            // labelCountGuide
            // 
            this.labelCountGuide.AutoSize = true;
            this.labelCountGuide.Location = new System.Drawing.Point(10, 39);
            this.labelCountGuide.Name = "labelCountGuide";
            this.labelCountGuide.Size = new System.Drawing.Size(66, 13);
            this.labelCountGuide.TabIndex = 7;
            this.labelCountGuide.Text = "Количество";
            // 
            // labelGuideName
            // 
            this.labelGuideName.AutoSize = true;
            this.labelGuideName.Location = new System.Drawing.Point(10, 15);
            this.labelGuideName.Name = "labelGuideName";
            this.labelGuideName.Size = new System.Drawing.Size(25, 13);
            this.labelGuideName.TabIndex = 6;
            this.labelGuideName.Text = "Гид";
            // 
            // FormAddGuides
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(343, 102);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxGuide);
            this.Controls.Add(this.labelCountGuide);
            this.Controls.Add(this.labelGuideName);
            this.Name = "FormAddGuides";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление гида";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.ComboBox comboBoxGuide;
        private System.Windows.Forms.Label labelCountGuide;
        private System.Windows.Forms.Label labelGuideName;
    }
}