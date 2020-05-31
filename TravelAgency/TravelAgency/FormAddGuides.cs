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
    public partial class FormAddGuides : Form
    {
        private readonly MainLogic mainLogic;
        private readonly IRequestLogic requestLogic;
        private readonly IGuideLogic guideLogic;
        private List<RequestViewModel> requestViews;
        private List<GuideViewModel> guideViews;

        public FormAddGuides(MainLogic mainLogic, IRequestLogic requestLogic, IGuideLogic guideLogic)
        {
            InitializeComponent();
            this.mainLogic = mainLogic;
            this.requestLogic = requestLogic;
            this.guideLogic = guideLogic;
            LoadData();
        }

        private void LoadData()
        {
            requestViews = requestLogic.Read(null);
            if (requestViews != null)
            {
                comboBoxHotels.DataSource = requestViews;
                comboBoxHotels.DisplayMember = "SupplierName";
            }
            guideViews = guideLogic.Read(null);
            if (guideViews != null)
            {
                comboBoxComponent.DataSource = guideViews;
                comboBoxComponent.DisplayMember = "GuideName";
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (textBoxCountComponent.Text == string.Empty)
                throw new Exception("Введите количество продуктов");

            mainLogic.ReplanishHotel(new ReserveGuideBindingModel()
            {
                HotelId = (comboBoxHotels.SelectedItem as HotelViewModel).Id,
                GuideId = (comboBoxComponent.SelectedItem as GuideViewModel).Id,
                Count = Convert.ToInt32(textBoxCountComponent.Text)
            });
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}