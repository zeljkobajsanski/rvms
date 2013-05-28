using System;
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewPromenaLozinke : ViewBase
    {
        private readonly PromenaLozinkeViewModel m_ViewModel;

        public ViewPromenaLozinke()
        {
            InitializeComponent();
            m_ViewModel = new PromenaLozinkeViewModel();
            promenaLozinkeViewModelBindingSource.DataSource = m_ViewModel;
        }

        public override void Sacuvaj()
        {
            try
            {
                m_ViewModel.Sacuvaj();
                OnNotify(new SavedMessage());
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }
    }
}
