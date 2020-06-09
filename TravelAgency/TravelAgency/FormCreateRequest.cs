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
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BusinessLogic;

namespace TravelAgency
{
    public partial class FormCreateRequest : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IRequestLogic requestLogic;
        private readonly ISupplierLogic supplierLogic;
        private readonly MainLogic mainLogic;
        public int ID { set { Id = value; } }
        private int? Id;
        private Dictionary<int, (string, int,bool)> requestGuides;

        public FormCreateRequest(MainLogic mainLogic,
            IRequestLogic requestLogic, ISupplierLogic supplierLogic)
        {
            InitializeComponent();
            this.requestLogic = requestLogic;
            this.supplierLogic = supplierLogic;
            this.mainLogic = mainLogic;
        }

        private void RequestCreationForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            if (Id.HasValue)
            {
                try
                {
                    RequestViewModel request = requestLogic.Read(new RequestBindingModel
                    {
                        Id = Id.Value
                    })?[0];
                    if (request != null)
                    {
                        comboBoxSupplier.SelectedIndex =
                            comboBoxSupplier.FindStringExact(request.SupplierFIO);
                        requestGuides = request.Guides;
                        LoadGuides();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Ошибка загрузки данных заявки",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                requestGuides = new Dictionary<int, (string, int,bool)>();
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                List<SupplierViewModel> suppliersList = supplierLogic.Read(null);
                if (suppliersList != null)
                {
                    comboBoxSupplier.DisplayMember = "SupplierFIO";
                    comboBoxSupplier.ValueMember = "Id";
                    comboBoxSupplier.DataSource = suppliersList;
                    comboBoxSupplier.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка загрузки списка поставщиков",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadGuides()
        {
            try
            {
                if (requestGuides != null)
                {
                    guidesGridView.Rows.Clear();
                    foreach (var requestGuide in requestGuides)
                    {
                        guidesGridView.Rows.Add(new object[] {
                            requestGuide.Key,
                            requestGuide.Value.Item1,
                            requestGuide.Value.Item2
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка загрузки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddGuideButton_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAddGuides>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (requestGuides.ContainsKey(form.Id))
                {
                    requestGuides[form.Id] = (form.GuideName, form.Count,false);
                }
                else
                {
                    requestGuides.Add(form.Id, (form.GuideName, form.Count,false));
                }
                LoadGuides();
            }
        }

        private void UpdateGuideButton_Click(object sender, EventArgs e)
        {
            if (guidesGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormAddGuides>();
                int Id = Convert.ToInt32(guidesGridView.SelectedRows[0].Cells[0].Value);
                form.Id = Id;
                form.Count = requestGuides[Id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    requestGuides[form.Id] = (form.GuideName, form.Count,false);
                    LoadGuides();
                }
            }
        }

        private void DeleteGuideButton_Click(object sender, EventArgs e)
        {
            if (guidesGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show(
                    "Действительно хотите удалить гида?",
                    "Требуется подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        requestGuides.Remove(Convert.ToInt32(guidesGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    LoadGuides();
                }
            }
        }

        private void RefreshGuidesButton_Click(object sender, EventArgs e)
        {
            LoadGuides();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (comboBoxSupplier.SelectedValue == null)
            {
                MessageBox.Show(
                    "Поставщик не выбран",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (requestGuides == null || requestGuides.Count == 0)
            {
                MessageBox.Show(
                    "Не выбрано ни одного гида",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            try
            {
                mainLogic.CreateOrUpdateRequest(new RequestBindingModel
                {
                    Id = Id,
                    SupplierId = Convert.ToInt32(comboBoxSupplier.SelectedValue),
                    Guides = requestGuides
                });
                MessageBox.Show(
                    "Сохранение заявки прошло успешно",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void СancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}