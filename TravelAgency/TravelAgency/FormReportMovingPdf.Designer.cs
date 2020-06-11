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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportGuideViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCreateToPdf = new System.Windows.Forms.Button();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ReportGuideViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportGuideViewModelBindingSource
            // 
            this.ReportGuideViewModelBindingSource.DataSource = typeof(TravelAgencyBusinessLogic.ViewModels.ReportGuideViewModel);
            // 
            // buttonCreate
            // 
            this.buttonCreate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCreate.Location = new System.Drawing.Point(441, 5);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(100, 28);
            this.buttonCreate.TabIndex = 2;
            this.buttonCreate.Text = "Сформировать";
            this.buttonCreate.UseVisualStyleBackColor = false;
            this.buttonCreate.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // buttonCreateToPdf
            // 
            this.buttonCreateToPdf.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCreateToPdf.Location = new System.Drawing.Point(573, 5);
            this.buttonCreateToPdf.Name = "buttonCreateToPdf";
            this.buttonCreateToPdf.Size = new System.Drawing.Size(91, 28);
            this.buttonCreateToPdf.TabIndex = 3;
            this.buttonCreateToPdf.Text = "В PDF";
            this.buttonCreateToPdf.UseVisualStyleBackColor = false;
            this.buttonCreateToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(251, 12);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(122, 20);
            this.dateTimePickerTo.TabIndex = 14;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(226, 13);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(19, 13);
            this.labelTo.TabIndex = 13;
            this.labelTo.Text = "по";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(78, 13);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(14, 13);
            this.labelFrom.TabIndex = 12;
            this.labelFrom.Text = "С";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(98, 11);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(122, 20);
            this.dateTimePickerFrom.TabIndex = 11;
            // 
            // reportViewer
            // 
            reportDataSource.Name = "DataSetMoving";
            reportDataSource.Value = this.ReportGuideViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "TravelAgency.ReportMoving.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(3, 38);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(720, 324);
            this.reportViewer.TabIndex = 15;
            // 
            // FormReportMovingPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(722, 359);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.buttonCreateToPdf);
            this.Controls.Add(this.buttonCreate);
            this.Name = "FormReportMovingPdf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Движение гидов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCreateToPdf;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.BindingSource ReportGuideViewModelBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}