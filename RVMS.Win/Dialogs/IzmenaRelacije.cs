using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RVMS.Model.Entities;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Dialogs
{
    public partial class IzmenaRelacije : DevExpress.XtraEditors.XtraForm
    {
        private readonly IzmenaRelacijeViewModel m_ViewModel;

        public IzmenaRelacije()
        {
            InitializeComponent();
            m_ViewModel = new IzmenaRelacijeViewModel();
            m_ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        public IzmenaRelacije(int idRelacije) : this()
        {
            try
            {
                m_ViewModel.UcitajMedjustanicnoRastojanje(idRelacije);
            }
            catch (Exception exc)
            {

            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                m_ViewModel.Init();
            }
            catch (Exception exc)
            {
            }
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "MedjustanicnoRastojanje":
                    medjustanicnoRastojanjeBindingSource.DataSource = m_ViewModel.MedjustanicnoRastojanje ?? (object)typeof(MedjustanicnoRastojanje);
                    break;
                case "Stajalista":
                    stajalisteDTOBindingSource.DataSource = m_ViewModel.Stajalista;
                    break;
                case "IsBusy":
                    Cursor = m_ViewModel.IsBusy ? Cursors.WaitCursor : Cursors.Default;
                    break;
            }
        }
    }
}