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
                
            }
        }

        private void IscrtajMapu()
        {
            webBrowser1.Url = new Uri(ApplicationContext.Current.WebServiceHome + "/Linije/KreirajLiniju/" + m_ViewModel.IdLinije);
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
            textEdit1.Focus();
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
            m_ViewModel.RefreshMap += (sender, args) => IscrtajMapu();
            gridView1.CellValueChanged += (s, e) =>
            {
                var stajaliste = gridView1.GetRow(e.RowHandle) as StajalisteLinije;
                m_ViewModel.Azuriraj(stajaliste);
            };
            repositoryItemCheckEdit1.EditValueChanged += (s, e) => gridView1.CloseEditor();
            //repositoryItemSpinEdit1.EditValueChanged += (s, e) => gridView1.CloseEditor();
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
                }
                catch (Exception exc)
                {
                    OnNotify(new Message(MessageType.Warning, exc.Message));
                }
            }
        }
    }
}
