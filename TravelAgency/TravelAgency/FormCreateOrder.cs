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
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;

namespace TravelAgency
{
    public partial class FormCreateOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ITourLogic logicP;
        private readonly MainLogic logicM;
        private readonly IRequestLogic requestLogic;
        private Dictionary<int, (string, int, bool)> requestGuides;
        public int ID { set { Id = value; } }
        private int? Id;
        private readonly ISupplierLogic supplierLogic;

        public FormCreateOrder(ITourLogic logicP, MainLogic logicM,
            IRequestLogic requestLogic, ISupplierLogic supplierLogic)
        {
            InitializeComponent();
            this.logicP = logicP;
            this.logicM = logicM;
            this.requestLogic = requestLogic;
            this.supplierLogic = supplierLogic;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            try
            {
                //Логика загрузки списка компонент в выпадающий список
                List<TourViewModel> listTours = logicP.Read(null);
                if (listTours != null)
                {
                    comboBoxTour.DisplayMember = "TourName";
                    comboBoxTour.ValueMember = "Id";
                    comboBoxTour.DataSource = listTours;
                    comboBoxTour.SelectedItem = null;
                }

                if (Id.HasValue)
                {
                    RequestViewModel request = requestLogic.Read(new RequestBindingModel
                    {
                        Id = Id.Value
                    })?[0];
                    if (request != null)
                    {
                        requestGuides = request.Guides;
                    }
                }
                else
                {
                    requestGuides = new Dictionary<int, (string, int, bool)>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxTour.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxTour.SelectedValue);
                    TourViewModel tour = logicP.Read(new TourBindingModel
                    {
                        Id = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * tour?.Price ?? 0).ToString();
                    foreach (var p in tour.TourGuides)
                    {
                        requestGuides.Add(p.Key, (tour.TourGuides[p.Key].Item1, tour.TourGuides[p.Key].Item2, false));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void LoadSuppliers()
        {
            try
            {
                List<SupplierViewModel> suppliersList = supplierLogic.Read(null);
                if (suppliersList != null)
                {
                    comboBoxSupplier.DisplayMember = "Login";
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

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxTour.SelectedValue == null)
            {
                MessageBox.Show("Выберите тур", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxSupplier.SelectedValue == null)
            {
                MessageBox.Show("Выберите поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicM.CreateOrder(new OrderBindingModel
                {
                    TourId = Convert.ToInt32(comboBoxTour.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();

                try
                {
                    logicM.CreateOrUpdateRequest(new RequestBindingModel
                    {
                        Id = Id,
                        SupplierId = Convert.ToInt32(comboBoxSupplier.SelectedValue),
                        Guides = requestGuides
                    });
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}