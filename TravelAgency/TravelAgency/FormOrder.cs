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
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.Enums;


namespace TravelAgency
{
    public partial class FormOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IRequestLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> orders;
        private RequestStatus statuses;

        public FormOrder(IRequestLogic service)
        {
            InitializeComponent();
            dataGridViewComponents.Columns.Add("Id", "Id");
            dataGridViewComponents.Columns.Add("GuideName", "Гиды");
            dataGridViewComponents.Columns.Add("Count", "Количество");
            dataGridViewComponents.Columns[0].Visible = false;
            dataGridViewComponents.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.logic = service;
        }

        private void FormDisplayStorageMaterials_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    RequestViewModel view = logic.Read(new RequestBindingModel { Id = id.Value })[0];
                    if (view != null)
                    {
                        orders = view.Guides;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                orders = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (orders != null)
                {
                    dataGridViewComponents.Rows.Clear();
                    foreach (var pc in orders)
                    {
                        dataGridViewComponents.Rows.Add(new object[] { "", pc.Key, pc.Value });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}