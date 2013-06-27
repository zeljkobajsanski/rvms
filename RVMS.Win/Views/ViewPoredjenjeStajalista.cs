using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using RVMS.Model.DTO;
using RVMS.Win.Messages;
using RVMS.Win.Models;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewPoredjenjeStajalista : ViewBase
    {
        private readonly PoredjenjeStajalistaViewModel m_ViewModel = new PoredjenjeStajalistaViewModel();

        public ViewPoredjenjeStajalista()
        {
            InitializeComponent();
            m_ViewModel.PropertyChanged += ViewModelPropertyChanged;
            stajalisteSaRelacijamaBindingSource.DataSource = m_ViewModel.IzabranaStajalista;
            HandleEvents();
            webBrowser1.Url = new Uri(ApplicationContext.Current.WebServiceHome + "/Stajalista/PoredjenjeStajalista");
        }

        

        protected override void OnLoad(EventArgs e)
        {
            m_ViewModel.Init();
        }

        public override void Osvezi()
        {
            m_ViewModel.Ocisti();
            m_ViewModel.Init();
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Stajalista":
                    stajalisteDTOBindingSource.DataSource = m_ViewModel.Stajalista;
                    break;
                case "NovoStajaliste":
                    DodajNovoStajalisteNaMapu();
                    break;
            }
        }

        private void DodajNovoStajalisteNaMapu()
        {
            if (webBrowser1.Document != null)
            {
                if (m_ViewModel.NovoStajaliste.ImaKoordinate)
                {
                    webBrowser1.Document.InvokeScript("dodajStajaliste",
                                                      new object[]
                                                      {
                                                          m_ViewModel.NovoStajaliste.Lat, m_ViewModel.NovoStajaliste.Lon
                                                          ,
                                                          m_ViewModel.NovoStajaliste.Naziv,
                                                          m_ViewModel.NovoStajaliste.Id
                                                      });
                }
                else
                {
                    webBrowser1.Document.InvokeScript("dodajOpstinu", new object[] {m_ViewModel.NovoStajaliste.Opstina});
                }
                
            }
        }

        private void HandleEvents()
        {
            gridView1.CustomUnboundColumnData += (s, e) =>
            {
                if (e.Column.Caption == "#")
                {
                    var stajaliste = e.Row as StajalisteDTO;
                    if (e.IsGetData)
                    {
                        e.Value = m_ViewModel.IzabranoStajaliste(stajaliste);
                    }
                    if (e.IsSetData)
                    {
                        var check = Convert.ToBoolean(e.Value);
                        if (check)
                        {
                            gridControl2.Invoke(new Action(() => m_ViewModel.DodajStajaliste(stajaliste)));
                        }
                        else
                        {
                            m_ViewModel.IzvadiStajaliste(stajaliste);
                            SkloniSaMape(stajaliste.Id);
                        }
                    }
                }
            };
            gridView2.CustomDrawCell += (s, e) =>
            {
                if (e.Column == colOpstina1)
                {
                    var stajaliste = gridView2.GetRow(e.RowHandle) as StajalisteSaRelacijama;
                    if (stajaliste != null && stajaliste.ImaKoordinate)
                    {
                        e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.marker_16x16,
                                                               new Rectangle(e.Bounds.Right - 16, e.Bounds.Y, 16, 16));
                    }
                    else
                    {
                        e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.marker_pink,
                                                               new Rectangle(e.Bounds.Right - 16, e.Bounds.Y, 16, 16));
                    }
                }
            };
            repositoryItemCheckEdit1.EditValueChanged += (s, e) => gridView1.CloseEditor();
            repositoryItemButtonEdit1.ButtonClick += (s, e) =>
            {
                var detailView = (GridView)gridView2.GetDetailView(gridView2.FocusedRowHandle, 0);
                var relacija = (Relacija)detailView.GetFocusedRow();
                switch (e.Button.Index)
                {
                    case 0:
                        OnRequestView(Views.ViewRelacije, relacija.Id);
                        break;
                    case 1:
                        ObeleziRelacijuNaMapi(relacija);
                        break;
                }
                
            };
            repositoryItemButtonEdit2.ButtonClick += (s, e) =>
            {
                var stajaliste = gridView2.GetFocusedRow() as StajalisteSaRelacijama;
                switch (e.Button.Index)
                {
                    case 0:
                        var msg = new QuestionMessage("Da li želite da proglasite podrazumevano stajalište?");
                        OnNotify(msg);
                        if (msg.Confirm)
                        {
                            m_ViewModel.ProglasiDefaultStajaliste(stajaliste);
                        }
                        break;
                    case 1:
                        msg = new QuestionMessage("Da li želite da obrišete stajalište iz baze?");
                        OnNotify(msg);
                        if (msg.Confirm)
                        {
                            try
                            {
                                m_ViewModel.ObrisiStajaliste(stajaliste);
                            }
                            catch (Exception exc)
                            {
                                OnNotify(new ErrorMessage(exc));
                            }
                        }
                        break;
                }
            };
            btnClear.Click += (s, e) =>
            {
                m_ViewModel.Ocisti();
                gridView1.RefreshData();
                if (webBrowser1.Document != null)
                {
                    webBrowser1.Document.InvokeScript("ocistiMarkere");
                }
            };
        }

        private void ObeleziRelacijuNaMapi(Relacija relacija)
        {
            
        }

        private void SkloniSaMape(int idStajalista)
        {
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.InvokeScript("skloniStajaliste", new object[] {idStajalista});
            }
        }
    }
}
