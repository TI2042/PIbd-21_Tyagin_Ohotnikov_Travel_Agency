namespace TravelAgency
{
    partial class FormRequest
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxComponents = new System.Windows.Forms.GroupBox();
            this.dataGridViewComponents = new System.Windows.Forms.DataGridView();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonContent = new System.Windows.Forms.Button();
            this.groupBoxComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComponents)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonOk.Location = new System.Drawing.Point(326, 236);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(72, 24);
            this.buttonOk.TabIndex = 13;
            this.buttonOk.Text = "Ок";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // groupBoxComponents
            // 
            this.groupBoxComponents.Controls.Add(this.dataGridViewComponents);
            this.groupBoxComponents.Location = new System.Drawing.Point(16, 11);
            this.groupBoxComponents.Name = "groupBoxComponents";
            this.groupBoxComponents.Size = new System.Drawing.Size(390, 219);
            this.groupBoxComponents.TabIndex = 11;
            this.groupBoxComponents.TabStop = false;
            this.groupBoxComponents.Text = "Гиды";
            // 
            // dataGridViewComponents
            // 
            this.dataGridViewComponents.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewComponents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComponents.Location = new System.Drawing.Point(8, 17);
            this.dataGridViewComponents.Name = "dataGridViewComponents";
            this.dataGridViewComponents.RowHeadersWidth = 51;
            this.dataGridViewComponents.Size = new System.Drawing.Size(374, 196);
            this.dataGridViewComponents.TabIndex = 0;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonUpdate.Location = new System.Drawing.Point(239, 236);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(72, 24);
            this.buttonUpdate.TabIndex = 14;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonContent
            // 
            this.buttonContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonContent.Location = new System.Drawing.Point(16, 236);
            this.buttonContent.Name = "buttonContent";
            this.buttonContent.Size = new System.Drawing.Size(111, 24);
            this.buttonContent.TabIndex = 15;
            this.buttonContent.Text = "Содержимое заказа";
            this.buttonContent.UseVisualStyleBackColor = false;
            this.buttonContent.Click += new System.EventHandler(this.buttonContent_Click);
            // 
            // FormRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(416, 272);
            this.Controls.Add(this.buttonContent);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxComponents);
            this.Name = "FormRequest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заказы гидов";
            this.Load += new System.EventHandler(this.FormDisplayStorageMaterials_Load);
            this.groupBoxComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComponents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBoxComponents;
        private System.Windows.Forms.DataGridView dataGridViewComponents;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonContent;
    }
}