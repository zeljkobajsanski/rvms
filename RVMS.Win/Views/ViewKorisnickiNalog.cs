using System.ComponentModel;
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewKorisnickiNalog : ViewBase
    {
        private readonly KorisnickiNalogViewModel m_ViewModel;

        public ViewKorisnickiNalog()
        {
            InitializeComponent();
            m_ViewModel = new KorisnickiNalogViewModel();
            m_ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            m_ViewModel.Init();
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "KorisnickiNalog":
                    korisnikBindingSource.DataSource = m_ViewModel.KorisnickiNalog;
                    break;
            }
        }

        public override void Sacuvaj()
        {
            try
            {
                m_ViewModel.Sacuvaj();
                OnNotify(new SavedMessage());
            }
            catch (System.Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }
    }
}
