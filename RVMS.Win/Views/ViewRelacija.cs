using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewRelacija : ViewBase
    {
        private RelacijaViewModel m_ViewModel;

        public ViewRelacija()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            m_ViewModel = new RelacijaViewModel();
            m_ViewModel.PropertyChanged += ModelPropertyChanged;
            relacijaViewModelBindingSource.DataSource = m_ViewModel;
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.Init();
            });
            task.ContinueWith(t =>
            {
                opstinaBindingSource.DataSource = m_ViewModel.Opstine;
                opstinaBindingSource1.DataSource = m_ViewModel.Opstine;
                IsBusy = false;
            });
            task.Start();
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ("IdPolazneOpstine" == e.PropertyName)
            {
                var task = new Task(() =>
                {
                    IsBusy = true;
                    m_ViewModel.UcitajPolazneStanice();
                });
                task.ContinueWith(t =>
                {
                    stajalisteDTOBindingSource.DataSource = m_ViewModel.PolaznaStajalista;
                    IsBusy = false;
                });
                task.Start();
            }
            if ("IdDolazneOpstine" == e.PropertyName)
            {
                var task = new Task(() =>
                {
                    IsBusy = true;
                    m_ViewModel.UcitajDolazneStanice();
                });
                task.ContinueWith(t =>
                {
                    stajalisteDTOBindingSource1.DataSource = m_ViewModel.DolaznaStajalista;
                    IsBusy = false;
                });
                task.Start();
            }
        }
    }
}
