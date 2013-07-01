using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using RVMS.Model.DTO;
using RVMS.Win.Messages;
using RVMS.Win.Models;
using RVMS.Win.ViewModels;
using Message = RVMS.Win.Messages.Message;

namespace RVMS.Win.Views
{
    public partial class ViewLinija : ViewBase
    {
        private readonly LinijaViewModel m_ViewModel = new LinijaViewModel();

        public ViewLinija()
        {
            InitializeComponent();
            HandleEvents();
            stajalisteLinijeBindingSource.DataSource = m_ViewModel.StajalistaLinije;
            linijaViewModelBindingSource.DataSource = m_ViewModel;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Stajalista":
                    stajalisteDTOBindingSource.DataSource = m_ViewModel.Stajalista;
                    break;
                case "Relacije":
                    relacijaDTOBindingSource.DataSource = m_ViewModel.Relacije;
                    break;
                case "DodataStajalista":
                    DodajMarkere(m_ViewModel.DodataStajalista);
                    break;
            }
        }

        private void DodajMarkere(IEnumerable<StajalisteLinije> stajalista)
        {
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.InvokeScript("dodajMarker"
                foreach (var stajalisteDto in stajalista)
                {
                    webBrowser1.Document.InvokeScript("dodajMarker",
                                                      new object[] { stajalisteDto.Id, stajalisteDto., stajalisteDto.Longituda });
                }
            }
            
            
        }

        protected override void OnLoad(EventArgs e)
        {
            m_ViewModel.Init();
            webBrowser1.Url = new Uri(ApplicationContext.Current.WebServiceHome + "/Linije/KreirajLiniju");
        }

        public override void NoviUnos()
        {
            m_ViewModel.NoviUnos();
            webBrowser1.Url = new Uri(ApplicationContext.Current.WebServiceHome + "/Linije/KreirajLiniju");
        }

        public override void Sacuvaj()
        {
            try
            {
                m_ViewModel.Sacuvaj();
            }
            catch (ValidationException)
            {
                OnNotify(new InvalidForSaveMessage());
            }
        }

        private void HandleEvents()
        {
            m_ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            lkpStajalista.ButtonClick += (s, e) =>
            {
                if (e.Button.Kind == ButtonPredefines.Plus)
                {
                    DodajStajaliste();
                }
            };
            lkpRelacije.ButtonClick += (s, e) =>
            {
                if (e.Button.Kind == ButtonPredefines.Plus)
                {
                    DodajRelaciju();
                }
            };
            repositoryItemButtonEdit1.ButtonClick += (s, e) => ObrisiStajaliste();
        }

        private void DodajRelaciju()
        {
            var val = lkpRelacije.EditValue;
            if (val != null && val != DBNull.Value)
            {
                var idRelacije = Convert.ToInt32(val);
                Invoke(new Action(() => m_ViewModel.DodajStajalistaRelacije(idRelacije)));
            }
        }

        private void DodajStajaliste()
        {
            var val = lkpStajalista.EditValue;
            if (val != null && val != DBNull.Value)
            {
                var idStajalista = Convert.ToInt32(val);
                try
                {
                    m_ViewModel.DodajStajaliste(idStajalista);
                    var stajaliste = lkpStajalista.GetSelectedDataRow() as StajalisteDTO;
                    if (webBrowser1.Document != null)
                    {
                        webBrowser1.Document.InvokeScript("dodajMarker",
                                                          new object[]
                                                          {stajaliste.Id, stajaliste.Latituda, stajaliste.Longituda});
                    }
                }
                catch (Exception exc)
                {
                    OnNotify(new ErrorMessage(exc));
                }
            }
        }

        private void ObrisiStajaliste()
        {
            var stajaliste = gridView1.GetFocusedRow() as StajalisteLinije;
            if (stajaliste != null)
            {
                try
                {
                    m_ViewModel.Obrisi(stajaliste);
                    if (webBrowser1.Document != null)
                    {
                        webBrowser1.Document.InvokeScript("obrisiMarker", new object[] {stajaliste.IdStajalista});
                    }
                }
                catch (Exception exc)
                {
                    OnNotify(new Message(MessageType.Warning, exc.Message));
                }
            }
        }
    }
}
