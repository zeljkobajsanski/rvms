using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RVMS.Model.DTO;
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewDaljinar : ViewBase
    {
        private readonly DaljinarViewModel m_ViewModel = new DaljinarViewModel();

        public ViewDaljinar()
        {
            InitializeComponent();
            relacijaDTOBindingSource.DataSource = m_ViewModel.Daljinar;
            repositoryItemButtonEdit1.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0) Izmeni();
            };
            m_ViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBusy")
            {
                IsBusy = m_ViewModel.IsBusy;
            }
            else if ("Daljinar" == e.PropertyName)
            {
                relacijaDTOBindingSource.DataSource = m_ViewModel.Daljinar;
            }
        }

        private void Izmeni()
        {
            var relacija = IzabranaRelacija();
            if (relacija != null)
            {
                OnRequestView(Views.ViewRelacije, relacija.Id);
            }
        }

        private RelacijaDTO IzabranaRelacija()
        {
            return gridView1.GetFocusedRow() as RelacijaDTO;
        }

        protected override void OnLoad(EventArgs e)
        {
            Osvezi();
        }

        public override void Osvezi()
        {
            try
            {
                m_ViewModel.UcitajDaljinar();
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }
    }
}
