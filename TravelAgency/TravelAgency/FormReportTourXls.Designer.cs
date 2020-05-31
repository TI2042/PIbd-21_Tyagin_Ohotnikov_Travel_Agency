namespace TravelAgency
{
    partial class FormReportTourXls
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
            this.dataGridViewGuideToTour = new System.Windows.Forms.DataGridView();
            this.buttonSaveToExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGuideToTour)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewGuideToTour
            // 
            this.dataGridViewGuideToTour.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewGuideToTour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGuideToTour.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewGuideToTour.Location = new System.Drawing.Point(0, 32);
            this.dataGridViewGuideToTour.Name = "dataGridViewGuideToTour";
            this.dataGridViewGuideToTour.Size = new System.Drawing.Size(536, 418);
            this.dataGridViewGuideToTour.TabIndex = 0;
            // 
            // buttonSaveToExcel
            // 
            this.buttonSaveToExcel.Location = new System.Drawing.Point(12, 3);
            this.buttonSaveToExcel.Name = "buttonSaveToExcel";
            this.buttonSaveToExcel.Size = new System.Drawing.Size(137, 23);
            this.buttonSaveToExcel.TabIndex = 1;
            this.buttonSaveToExcel.Text = "Сохранить в Excel";
            this.buttonSaveToExcel.UseVisualStyleBackColor = true;
            this.buttonSaveToExcel.Click += new System.EventHandler(this.ButtonSaveToExcel_Click);
            // 
            // FormReportTourXls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 450);
            this.Controls.Add(this.buttonSaveToExcel);
            this.Controls.Add(this.dataGridViewGuideToTour);
            this.Name = "FormReportTourXls";
            this.Text = "Заказы туров";
            this.Load += new System.EventHandler(this.FormReportGuidesToTours_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGuideToTour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewGuideToTour;
        private System.Windows.Forms.Button buttonSaveToExcel;
    }
}