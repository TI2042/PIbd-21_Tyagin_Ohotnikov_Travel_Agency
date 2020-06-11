using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using TravelAgencyBusinessLogic.BusinessLogic;
using Microsoft.Reporting.WinForms;
using TravelAgencyBusinessLogic.BindingModels;

namespace TravelAgency
{
    public partial class FormReportMovingPdf : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportMovingPdf(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;

        }

        private void ButtonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerTo.Value.Date > dateTimePickerFrom.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var dataSource = logic.GetGuides(dateTimePickerTo.Value.Date, dateTimePickerFrom.Value.Date);
                ReportDataSource source = new ReportDataSource("DataSetMoving", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonToPdf_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveGuidesToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateTo = dateTimePickerTo.Value.Date,
                            DateFrom = dateTimePickerFrom.Value.Date
                        });
                        MessageBox.Show("Отчет отправлен на почту", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormReportTourGuides_Load(object sender, EventArgs e)
        {
            this.reportViewer.RefreshReport();
        }
    }
}