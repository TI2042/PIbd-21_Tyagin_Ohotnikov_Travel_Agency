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
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyBusinessLogic.BindingModels;

namespace TravelAgency
{
    public partial class FormAddGuides : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxGuide.SelectedValue); }
            set { comboBoxGuide.SelectedValue = value; }
        }
        public string GuideName { get { return comboBoxGuide.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set { textBoxCount.Text = value.ToString(); }
        }

        public FormAddGuides(IGuideLogic logic)
        {
            InitializeComponent();
            List<GuideViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxGuide.DisplayMember = "GuideName";
                comboBoxGuide.ValueMember = "Id";
                comboBoxGuide.DataSource = list;
                comboBoxGuide.SelectedItem = null;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show(
                    "Поле \"Количество\" не заполнено",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (comboBoxGuide.SelectedValue == null)
            {
                MessageBox.Show(
                    "Не выбран гид",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
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