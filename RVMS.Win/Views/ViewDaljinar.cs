﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            repositoryItemButtonEdit1.ButtonClick += (s, e) =>
            {
                if (e.Button.Index == 0) Izmeni();
                else if (e.Button.Index == 1) Obrisi();
            };
            m_ViewModel.PropertyChanged += ViewModelPropertyChanged;
            daljinarViewModelBindingSource.DataSource = m_ViewModel;
            searchLookUpEdit1.ButtonClick += (s, e) =>
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    searchLookUpEdit1.EditValue = null;
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
            } else if ("Stajalista" == e.PropertyName)
            {
                stajalisteDTOBindingSource.DataSource = m_ViewModel.Stajalista;
            } else if ("IzabranoStajaliste" == e.PropertyName)
            {
                m_ViewModel.UcitajDaljinar();
            } else if ("TipStajalista" == e.PropertyName)
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
    }
}
