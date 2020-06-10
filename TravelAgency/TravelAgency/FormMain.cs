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
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgency
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly MainLogic logic;
        private readonly IOrderLogic orderLogic;
        private readonly ReportLogic report;
        private readonly IRequestLogic requestLogic;
        private readonly ITourLogic tourLogic;
        private readonly IGuideLogic guideLogic;

        public FormMain(MainLogic logic, IOrderLogic orderLogic, ReportLogic report, IGuideLogic guideLogic, IRequestLogic requestLogic, ITourLogic tourLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.orderLogic = orderLogic;
            this.report = report;
            this.tourLogic = tourLogic;
            this.requestLogic = requestLogic;
            this.guideLogic = guideLogic;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var listOrders = orderLogic.Read(null);
            if (listOrders != null)
            {
                dataGridView.DataSource = listOrders;
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView.Columns[2].Visible = false;
                dataGridView.Columns[7].Visible = false;
            }
            dataGridView.Update();
        }

        private void GuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormGuides>();
            form.ShowDialog();
        }

        private void ToursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTours>();
            form.ShowDialog();
        }

        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void ButtonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    logic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    logic.FinishOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    logic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void гидыPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportMovingPdf>();
            form.ShowDialog();
        }

        private void турыDocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        report.SaveOrdersToWordFile(new ReportBindingModel
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

        private void блюдаXlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportTourXls>();
            form.ShowDialog();
        }

        private void заказатьПродуктыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateRequest>();
            form.ShowDialog();
        }

        private void посмотретьДоступныеПродуктыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRequest>();
            form.ShowDialog();
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    orderLogic.SaveXml(fbd.SelectedPath);
                    orderLogic.SaveXml(fbd.SelectedPath);
                    guideLogic.SaveXmlGuide(fbd.SelectedPath);
                    tourLogic.SaveXml(fbd.SelectedPath);
                    requestLogic.SaveXmlRequest(fbd.SelectedPath);
                    report.SendMailReport("den.ohotnikov@gmail.com", fbd.SelectedPath, "XML бекап", "xml");
                    MessageBox.Show("Бэкап отправлен на почту", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void jSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    orderLogic.SaveJson(fbd.SelectedPath);
                    orderLogic.SaveJson(fbd.SelectedPath);
                    guideLogic.SaveJsonGuide(fbd.SelectedPath);
                    tourLogic.SaveJson(fbd.SelectedPath);
                    requestLogic.SaveJsonRequest(fbd.SelectedPath);
                    report.SendMailReport("den.ohotnikov@gmail.com", fbd.SelectedPath, "JSON бекап", "json");
                    MessageBox.Show("Бэкап отправлен на почту", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}