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
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;
using Message = RVMS.Win.Messages.Message;

namespace RVMS.Win.Views
{
    public partial class ViewRelacija : ViewBase
    {
        private RelacijaViewModel m_ViewModel;

        private Mapa m_Mapa;

        private string m_WebServiceHome;

        public ViewRelacija()
        {
            InitializeComponent();
            m_ViewModel = new RelacijaViewModel();
            m_ViewModel.PropertyChanged += ModelPropertyChanged;
            relacijaViewModelBindingSource.DataSource = m_ViewModel;
            Enable(false);
            HandleEvents();
            m_WebServiceHome = ConfigurationManager.AppSettings["WebserviceHome"];
            txtNazivRelacije.Focus();
        }

        public ViewRelacija(object param) : this()
        {
            var idRelacije = Convert.ToInt32(param);
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.UcitajRelaciju(idRelacije);
            });
            task.ContinueWith(task1 =>
            {
                if (task1.Exception != null)
                {
                    OnNotify(new ErrorMessage(task1.Exception));
                    return;
                }
                Invoke(new Action(() =>
                {
                    txtNazivRelacije.Refresh();
                    medjustanicnoRastojanjeDTOBindingSource.DataSource = m_ViewModel.MedjustanicnaRastojanja;
                    Enable(m_ViewModel.Relacija.IdRelacije != 0);
                }));
                
                IsBusy = false;
            });
            task.Start();
        }

        public override void Sacuvaj()
        {
            if (!m_ViewModel.IsValid)
            {
                OnNotify(new InvalidForSaveMessage());
                return;
            }
            var t = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.Sacuvaj();
            });
            t.ContinueWith((task) =>
            {
                IsBusy = false;
                Invoke(new Action(() => Enable(m_ViewModel.Relacija.IdRelacije != 0)));
            });
            t.Start();
        }

        public override void NoviUnos()
        {
            m_ViewModel.NoviUnos();
            Enable(false);
            txtNazivRelacije.Focus();
        }

        public override void Osvezi()
        {
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.Init();
                m_ViewModel.OsveziRelaciju();
            });
            task.ContinueWith(t =>
            {
                opstinaBindingSource.DataSource = m_ViewModel.Opstine;
                opstinaBindingSource1.DataSource = m_ViewModel.Opstine;
                stajalisteDTOBindingSource.DataSource = m_ViewModel.PolaznaStajalista;
                stajalisteDTOBindingSource1.DataSource = m_ViewModel.DolaznaStajalista;
                IsBusy = false;
            });
            task.Start();
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
            if ("IdDolaznogStajalista" == e.PropertyName)
            {
                
            }
            if ("MedjustanicnaRastojanja" == e.PropertyName)
            {
                Invoke(
                    new Action(
                        () =>
                        {
                            medjustanicnoRastojanjeDTOBindingSource.DataSource = m_ViewModel.MedjustanicnaRastojanja;
                        }));

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
                if (e.Button.Index == 0)
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
                    m_Mapa = new Mapa(m_WebServiceHome + "/Relacije/MapaRelacije/" + m_ViewModel.Relacija.IdRelacije);
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
        }
    }
}
