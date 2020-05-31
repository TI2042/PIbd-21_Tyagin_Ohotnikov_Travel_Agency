namespace TravelAgency
{
    partial class FormReportMovingPdf
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportOrdersViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCreateToPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrdersViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportOrdersViewModelBindingSource
            // 
            this.ReportOrdersViewModelBindingSource.DataSource = typeof(TravelAgencyBusinessLogic.ViewModels.ReportOrdersViewModel);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(189, 3);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(93, 20);
            this.buttonCreate.TabIndex = 2;
            this.buttonCreate.Text = "Сформировать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // buttonCreateToPdf
            // 
            this.buttonCreateToPdf.Location = new System.Drawing.Point(319, 3);
            this.buttonCreateToPdf.Name = "buttonCreateToPdf";
            this.buttonCreateToPdf.Size = new System.Drawing.Size(84, 20);
            this.buttonCreateToPdf.TabIndex = 3;
            this.buttonCreateToPdf.Text = "В PDF";
            this.buttonCreateToPdf.UseVisualStyleBackColor = true;
            this.buttonCreateToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // reportViewer
            // 
            reportDataSource1.Name = "DataSetMoving";
            reportDataSource1.Value = this.ReportOrdersViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "TravelAgency.ReportMoving.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(2, 32);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(602, 328);
            this.reportViewer.TabIndex = 4;
            this.reportViewer.Load += new System.EventHandler(this.FormReportTourGuides_Load);
            // 
            // FormReportMovingPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 360);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.buttonCreateToPdf);
            this.Controls.Add(this.buttonCreate);
            this.Name = "FormReportMovingPdf";
            this.Text = "Гиды по турам";
            this.Load += new System.EventHandler(this.FormReportTourGuides_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrdersViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCreateToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportOrdersViewModelBindingSource;
    }
}