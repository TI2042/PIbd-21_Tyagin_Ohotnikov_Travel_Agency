using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Unity;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BusinessLogic;

namespace TravelAgency
{
    public partial class FormReportTourXls : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportTourXls(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            dataGridViewGuideToTour.Columns.Add("Туры", "Туры");
            dataGridViewGuideToTour.Columns.Add("Гиды", "Гиды");
            dataGridViewGuideToTour.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void FormReportGuidesToTours_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetTourGuides();
                if (dict != null)
                {
                    Dictionary<string, List<ReportTourGuideViewModel>> tourGuides = new Dictionary<string, List<ReportTourGuideViewModel>>();
                    dataGridViewGuideToTour.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        if (!tourGuides.ContainsKey(elem.TourName))
                            tourGuides.Add(elem.TourName, new List<ReportTourGuideViewModel>() { elem });
                        else
                            tourGuides[elem.TourName].Add(elem);
                    }
                    foreach (var order in tourGuides)
                    {
                        dataGridViewGuideToTour.Rows.Add(order.Key, "", "");
                        foreach (var tour in order.Value)
                        {
                            dataGridViewGuideToTour.Rows.Add("", tour.TourName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveOrdersToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
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
    }
}