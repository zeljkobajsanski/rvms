using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using RVMS.Model.DTO;
using RVMS.Win.Dialogs;
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;
using Message = RVMS.Win.Messages.Message;

namespace RVMS.Win.Views
{
    public sealed partial class ViewRelacija : ViewBase
    {
        private RelacijaViewModel m_ViewModel;

        private Mapa m_Mapa;

        public ViewRelacija()
        {
            InitializeComponent();
            m_ViewModel = new RelacijaViewModel();
            opstinaBindingSource.DataSource = m_ViewModel.Opstine;
            opstinaBindingSource1.DataSource = m_ViewModel.Opstine;
            stajalisteDTOBindingSource.DataSource = m_ViewModel.PolaznaStajalista;
            stajalisteDTOBindingSource1.DataSource = m_ViewModel.DolaznaStajalista;
            m_ViewModel.PropertyChanged += ModelPropertyChanged;
            relacijaViewModelBindingSource.DataSource = m_ViewModel;
            Enable(false);
            HandleEvents();
            txtNazivRelacije.Focus();
        }

        public ViewRelacija(object param) : this()
        {
            var idRelacije = Convert.ToInt32(param);
            try
            {
                m_ViewModel.UcitajRelaciju(idRelacije);
                txtNazivRelacije.Refresh();
                medjustanicnoRastojanjeDTOBindingSource.DataSource = m_ViewModel.MedjustanicnaRastojanja;
                
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }

        public override void Sacuvaj()
        {
            if (!m_ViewModel.IsValid)
            {
                OnNotify(new InvalidForSaveMessage());
                return;
            }
            m_ViewModel.Sacuvaj();
            Enable(m_ViewModel.Relacija.IdRelacije != 0);
        }

        public override void NoviUnos()
        {
            m_ViewModel.NoviUnos();
            Enable(false);
            txtNazivRelacije.Focus();
        }

        public override void Otvori()
        {
            openFileDialog1.ShowDialog(this);
            var file = openFileDialog1.FileName;
            if (file != null)
            {
                dockPanel1.Show();
                axAcroPDF1.LoadFile(file);    
            }
        }

        public override void Osvezi()
        {
            try
            {
                m_ViewModel.OsveziRelaciju();
                m_ViewModel.Init();
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Osvezi();
        }

        private void PonistiLookup(object sender, ButtonPressedEventArgs e)
        {
            var lkp = (LookUpEdit) sender;
            if (e.Button.Index == 0)
            {
                lkp.EditValue = null;
            }
        }

        private void Dodaj()
        {
            m_ViewModel.Dodaj();
            OnNotify(new SavedMessage());
            dolaznoStajaliste.Focus();
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IdPolazneOpstine":
                    try
                    {
                        m_ViewModel.UcitajPolazneStanice();
                    }
                    catch (Exception exc)
                    {
                        OnNotify(new ErrorMessage(exc));
                    }
                    break;
                case "IdDolazneOpstine":
                    try
                    {
                        m_ViewModel.UcitajDolazneStanice();
                    }
                    catch (Exception exc)
                    {
                        OnNotify(new ErrorMessage(exc));
                    }
                    break;
                case "IdDolaznogStajalista":
                    break;
                case "MedjustanicnaRastojanja":
                    medjustanicnoRastojanjeDTOBindingSource.DataSource = m_ViewModel.MedjustanicnaRastojanja;
                    break;
                case "IsBusy":
                    IsBusy = m_ViewModel.IsBusy;
                    break;
                case "Relacija":
                    Enable(m_ViewModel.Relacija.IdRelacije != 0);
                    break;
                case "Opstine":
                    opstinaBindingSource.DataSource = m_ViewModel.Opstine;
                    opstinaBindingSource1.DataSource = m_ViewModel.Opstine;
                    break;
                case "PolaznaStajalista":
                    stajalisteDTOBindingSource.DataSource = m_ViewModel.PolaznaStajalista;
                    break;
                case "DolaznaStajalista":
                    stajalisteDTOBindingSource1.DataSource = m_ViewModel.DolaznaStajalista;
                    break;
            }
        }

        private void Enable(bool enable)
        {
            polazneOpstine.Enabled = enable;
            polaznoStajaliste.Enabled = enable;
            dolazneOpstine.Enabled = enable;
            dolaznoStajaliste.Enabled = enable;
            txtRazdaljina.Enabled = enable;
            txtVremeVoznje.Enabled = enable;
            simpleButton1.Enabled = enable;
            gridView1.OptionsBehavior.Editable = enable;
            
        }

        private void HandleEvents()
        {
            simpleButton1.Click += (s, e) => Dodaj();
            polazneOpstine.ButtonClick += PonistiLookup;
            dolazneOpstine.ButtonClick += PonistiLookup;
            gridView1.CellValueChanged += (s, e) =>
            {
                var rastojanje = (MedjustanicnoRastojanjeDTO)gridView1.GetRow(e.RowHandle);
                m_ViewModel.Sacuvaj(rastojanje);
                OnNotify(new SavedMessage());
            };
            repositoryItemButtonEdit1.ButtonClick += (s, e) =>
            {
                switch (e.Button.Index)
                {
                    case 1:
                    {
                        var question = new QuestionMessage("Da li želite da obrišete stavku?");
                        OnNotify(question);
                        if (question.Confirm)
                        {
                            var rastojanje = gridView1.GetFocusedRow() as MedjustanicnoRastojanjeDTO;
                            if (rastojanje != null)
                            {
                                m_ViewModel.Obrisi(rastojanje);
                            }
                        }
                    }
                        break;
                    case 0:
                        IzmeniSelektovanoRastojanje();
                        break;
                }
            };
            gridView1.CustomSummaryCalculate += (s, e) =>
            {
                var gsi = (GridSummaryItem)e.Item;
                switch (gsi.FieldName)
                {
                    case "PolaznoStajaliste":
                        e.TotalValue = m_ViewModel.UkupnaDuzinaRelacije;
                        break;
                    case "DolaznoStajaliste":
                        e.TotalValue = m_ViewModel.UkupnoVremeVoznje;
                        break;
                    case "Rastojanje":
                        e.TotalValue = m_ViewModel.SrednjaSaobracajnaBrzina;
                        break;
                }
            };
            gridView1.CustomDrawCell += (s, e) =>
            {
                var rastojanje = (MedjustanicnoRastojanjeDTO) gridView1.GetRow(e.RowHandle);
                if (e.Column.FieldName == "PolaznoStajaliste" && rastojanje.LatitudaPolaznogStajalista.HasValue &&
                    rastojanje.LongitudaPolaznogStajalista.HasValue)
                {
                    e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.globe, new Rectangle(e.Bounds.Right - 16, e.Bounds.Y, 16, 16));
                }
                if (e.Column.FieldName == "DolaznoStajaliste" && rastojanje.LatitudaDolaznogStajalista.HasValue &&
                    rastojanje.LongitudaDolaznogStajalista.HasValue)
                {
                    e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.globe, new Rectangle(e.Bounds.Right - 16, e.Bounds.Y, 16, 16));
                }
            };
            polaznoStajaliste.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0)
                {
                    m_ViewModel.OsveziPolaznaStajalista();
                }
            };
            dolaznoStajaliste.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0)
                {
                    m_ViewModel.OsveziDolaznaStajalista();
                }
            };
            btnMapa.Click += (s, e) =>
            {
                if (m_Mapa == null)
                {
                    m_Mapa = new Mapa(ApplicationContext.Current.WebServiceHome + "/Relacije/MapaRelacije/" + m_ViewModel.Relacija.IdRelacije);
                    m_Mapa.Closed += (s1, e1) => m_Mapa = null;
                }
                m_Mapa.Show(this);
            };
            gridView1.RowCellClick += (sender, args) =>
            {
                var rastojanje = (MedjustanicnoRastojanjeDTO) gridView1.GetRow(args.RowHandle);
                if (rastojanje != null && m_Mapa != null)
                {
                    if (args.Column.FieldName == "PolaznoStajaliste")
                    {
                        if (rastojanje.LatitudaPolaznogStajalista.HasValue && rastojanje.LongitudaPolaznogStajalista.HasValue)
                        {
                            OnNotify(new Message(MessageType.Ok, "Stajalište je već mapirano"));
                            return;
                        }
                        m_Mapa.InvokeScript("dodajStajaliste", rastojanje.PolaznoStajalisteId, rastojanje.PolaznoStajaliste);
                    }
                    if (args.Column.FieldName == "DolaznoStajaliste")
                    {
                        if (rastojanje.LatitudaDolaznogStajalista.HasValue && rastojanje.LongitudaDolaznogStajalista.HasValue)
                        {
                            OnNotify(new Message(MessageType.Ok, "Stajalište je već mapirano"));
                            return;
                        }
                        m_Mapa.InvokeScript("dodajStajaliste", rastojanje.DolaznoStajalisteId, rastojanje.DolaznoStajaliste);
                    }
                }
            };
            repositoryItemButtonEdit2.ButtonClick += (s, e) =>
            {
                var rastojanje =
                    (MedjustanicnoRastojanjeDTO)gridView1.GetFocusedRow();
                switch (e.Button.Kind)
                {
                    case ButtonPredefines.Up:
                        m_ViewModel.PomeriMedjustanicnoRastojanjeGore(
                            rastojanje);
                        break;
                    case ButtonPredefines.Down:
                        m_ViewModel.PomeriMedjustanicnoRastojanjeDole(
                            rastojanje);
                        break;
                }
            };
        }

        private void IzmeniSelektovanoRastojanje()
        {
            var rastojanje = (MedjustanicnoRastojanjeDTO) gridView1.GetFocusedRow();
            if (rastojanje != null)
            {
                using (var d = new IzmenaRelacije(rastojanje.Id))
                {
                    var r = d.ShowDialog(this);
                    if (DialogResult.OK == r)
                    {
                        try
                        {
                            m_ViewModel.OsveziRelaciju();
                        }
                        catch (Exception exc)
                        {
                            OnNotify(new ErrorMessage(exc));
                        }
                    }
                }
            }
        }
    }
}
