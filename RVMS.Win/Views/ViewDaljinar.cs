using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using RVMS.Model.DTO;
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewDaljinar : ViewBase
    {

        private readonly DaljinarViewModel m_ViewModel = new DaljinarViewModel();

        public ViewDaljinar()
        {
            InitializeComponent();
            radioGroup1.Properties.Items.Add(new RadioGroupItem(1, "Polazno stajalište"));
            radioGroup1.Properties.Items.Add(new RadioGroupItem(2, "Dolazno stajalište"));
            relacijaDTOBindingSource.DataSource = m_ViewModel.Daljinar;
            daljinarViewModelBindingSource.DataSource = m_ViewModel;
            HandleEvents();
        }

        public override void Osvezi()
        {
            try
            {
                m_ViewModel.UcitajStajalista();
                m_ViewModel.UcitajDaljinar();
            }
            catch (Exception exc)
            {
                OnNotify(new ErrorMessage(exc));
            }
        }

        public override void NoviUnos()
        {
            OnRequestView("Relacija", null);
        }

        private void HandleEvents()
        {
            repositoryItemButtonEdit1.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0)
                {
                    Izmeni();
                }
                else if (e.Button.Index == 1)
                {
                    Obrisi();
                }
            };
            m_ViewModel.PropertyChanged += ViewModelPropertyChanged;
            searchLookUpEdit1.ButtonClick += (s, e) =>
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    searchLookUpEdit1.EditValue = null;
                }
            };
            toolTipController1.GetActiveObjectInfo += (s, e) =>
            {
                if (e.SelectedControl == gridControl1)
                {
                    var hit = gridView1.CalcHitInfo(e.ControlMousePosition);
                    if (hit.InRow)
                    {
                        var relacija = gridView1.GetRow(hit.RowHandle) as RelacijaDTO;
                        if (relacija != null)
                        {
                            e.Info = new ToolTipControlInfo(){Title = relacija.Naziv};
                            e.Info.Text = m_ViewModel.VratiTooltip(relacija.Id);
                            e.Info.Object = hit.HitTest + hit.RowHandle;
                            //var tooltipArgs = new SuperToolTipSetupArgs();
                            //tooltipArgs.Title.Text = "Relacija";
                            //tooltipArgs.Contents.Text = "Relacija ID: " + relacija.Id;
                            //e.Info.ToolTipType = ToolTipType.SuperTip;
                            //e.Info.SuperTip = new SuperToolTip();
                            //e.Info.SuperTip.Setup(tooltipArgs);
                        }
                    }
                }
            };
        }


        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBusy")
            {
                IsBusy = m_ViewModel.IsBusy;
            }
            else if ("Daljinar" == e.PropertyName)
            {
                relacijaDTOBindingSource.DataSource = m_ViewModel.Daljinar;
            }
            else if ("Stajalista" == e.PropertyName)
            {
                stajalisteDTOBindingSource.DataSource = m_ViewModel.Stajalista;
            }
            else if ("IzabranoStajaliste" == e.PropertyName)
            {
                m_ViewModel.UcitajDaljinar();
            }
            else if ("TipStajalista" == e.PropertyName)
            {
                m_ViewModel.UcitajDaljinar();
            }
        }

        private void Izmeni()
        {
            var relacija = IzabranaRelacija();
            if (relacija != null)
            {
                OnRequestView(Views.ViewRelacije, relacija.Id);
            }
        }

        private void Obrisi()
        {
            var relacija = IzabranaRelacija();
            if (relacija != null)
            {
                var q = new QuestionMessage("Da li želite da obrišete relaciju?");
                OnNotify(q);
                if (q.Confirm)
                {
                    try
                    {
                        m_ViewModel.ObrisiRelaciju(relacija.Id);
                        relacijaDTOBindingSource.ResetBindings(true);
                    }
                    catch (Exception exc)
                    {
                        OnNotify(new ErrorMessage(exc));
                    }
                }
            }
        }

        private RelacijaDTO IzabranaRelacija()
        {
            return gridView1.GetFocusedRow() as RelacijaDTO;
        }

        protected override void OnLoad(EventArgs e)
        {
            Osvezi();
        }
    }
}
