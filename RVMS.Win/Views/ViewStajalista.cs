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
using Message = RVMS.Win.Messages.Message;

namespace RVMS.Win.Views
{
    public partial class ViewStajalista : ViewBase
    {
        private readonly StajalistaViewModel m_ViewModel = new StajalistaViewModel();

        public ViewStajalista()
        {
            InitializeComponent();
            opstinaBindingSource.DataSource = m_ViewModel.Opstine;
            mestoBindingSource.DataSource = m_ViewModel.Mesta;
            m_ViewModel.PropertyChanged += ViewModelPropertyChanged;
            stajalistaViewModelBindingSource.DataSource = m_ViewModel;
            stajalisteDTOBindingSource.DataSource = m_ViewModel.Stajalista;
            HandleEvents();
        }

        public override void Sacuvaj()
        {
            Dodaj();
        }

        public override void Osvezi()
        {
            OsveziStajalista();
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                m_ViewModel.Init();
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }

        private void HandleEvents()
        {
            opstine.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0)
                {
                    OsveziOpstine();
                }
                if (e.Button.Index == 1)
                {
                    m_ViewModel.IdOpstine = null;
                }
            };
            mesta.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0)
                {
                    OsveziMestaIStajalista();
                }
                if (e.Button.Index == 1)
                {
                    m_ViewModel.IdMesta = null;
                }
            };
            btnDodaj.Click += (s, e) => Dodaj();
            gridView1.CellValueChanged += (s, e) =>
            {
                var stajaliste = (StajalisteDTO) gridView1.GetRow(e.RowHandle);
                if (stajaliste != null)
                {
                    try
                    {
                        m_ViewModel.Update(stajaliste);
                        OnNotify(new SavedMessage());
                    }
                    catch (Exception exc)
                    {
                        OnNotify(new ErrorMessage(exc));
                    }
                }
            };
            repositoryItemButtonEdit1.ButtonClick += (s, e) =>
            {
                switch (e.Button.Index)
                {
                    case 0:
                        MapirajStajaliste();
                        break;
                    case 1:
                        ObrisiKoordinate();
                        break;
                    case 2:
                        ObrisiStajaliste();
                        break;
                }
                
            };
            repositoryItemCheckEdit1.EditValueChanged += (s, e) => gridView1.CloseEditor();
        }

        private void ObrisiStajaliste()
        {
            var q = new QuestionMessage("Da li želite da obrišete stajalište?");
            OnNotify(q);
            if (q.Confirm)
            {
                var stajaliste = (StajalisteDTO)gridView1.GetFocusedRow();
                try
                {
                    m_ViewModel.Obrisi(stajaliste);
                    OnNotify(new SavedMessage());
                }
                catch (Exception exc)
                {
                    OnNotify(new ErrorMessage(exc));
                }
            }
        }

        private void ObrisiKoordinate()
        {
            var q = new QuestionMessage("Da li želite da obrišete koordinate stajališta?");
            OnNotify(q);
            if (q.Confirm)
            {
                var stajaliste = (StajalisteDTO)gridView1.GetFocusedRow();
                stajaliste.Latituda = null;
                stajaliste.Longituda = null;
                try
                {
                    m_ViewModel.Update(stajaliste);
                    OnNotify(new SavedMessage());
                }
                catch (Exception exc)
                {
                    OnNotify(new ErrorMessage(exc));
                }  
            }
        }

        private void MapirajStajaliste()
        {
            var stajaliste = (StajalisteDTO)gridView1.GetFocusedRow();
            using (var m = new Mapa(ApplicationContext.Current.WebServiceHome + "/Stajalista/MapaStajalista/" + stajaliste.Id))
            {
                m.ShowDialog(this);
            }
        }

        private void Dodaj()
        {
            var msg = m_ViewModel.Dodaj();
            if (msg != null)
            {
                OnNotify(new Message(MessageType.Warning, msg));
                return;
            }
            OnNotify(new SavedMessage());
            txtNaziv.Focus();
        }

        private void OsveziOpstine()
        {
            try
            {
                m_ViewModel.UcitajOpstine();
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }

        private void OsveziStajalista()
        {
            try
            {
                m_ViewModel.UcitajStajalista();
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }

        private void OsveziMestaIStajalista()
        {
            try
            {
                m_ViewModel.UcitajMesta();
                m_ViewModel.UcitajStajalista();
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IdOpstine":
                    OsveziMestaIStajalista();
                    break;
                case "IdMesta":
                    OsveziStajalista();
                    break;
                case "IsBusy":
                    IsBusy = m_ViewModel.IsBusy;
                    break;
                case "Stajalista":
                    stajalisteDTOBindingSource.ResetBindings(true);
                    break;
                case "Opstine":
                    opstinaBindingSource.DataSource = m_ViewModel.Opstine;
                    break;
                case "Mesta":
                    mestoBindingSource.DataSource = m_ViewModel.Mesta;
                    break;
            }
        }
    }
}
