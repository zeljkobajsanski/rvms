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
            repositoryItemButtonEdit1.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0) Izmeni();
            };
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
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.UcitajDaljinar();
            });
            task.ContinueWith((t) =>
            {
                Invoke(new Action(() =>
                {
                    if (t.Exception != null)
                    {
                        OnNotify(new ErrorMessage(t.Exception));
                    }
                    relacijaDTOBindingSource.DataSource = m_ViewModel.Daljinar;
                }));
                IsBusy = false;
            });
            task.Start();
        }
    }
}
