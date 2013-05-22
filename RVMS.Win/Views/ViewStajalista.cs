﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;
using Message = RVMS.Win.Messages.Message;

namespace RVMS.Win.Views
{
    public partial class ViewStajalista : ViewBase
    {
        private StajalistaViewModel m_ViewModel = new StajalistaViewModel();

        public ViewStajalista()
        {
            InitializeComponent();
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
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.Init();
            });
            task.ContinueWith(t =>
            {
                opstinaBindingSource.DataSource = m_ViewModel.Opstine;
                IsBusy = false;
            });
            task.Start();
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
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.UcitajOpstine();
            });
            task.ContinueWith(t =>
            {
                opstinaBindingSource.DataSource = m_ViewModel.Opstine;
                IsBusy = false;
            });
            task.Start();
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
            }
        }

        private void OsveziStajalista()
        {
            var task = new Task(() =>
            {
                IsBusy = true;
                Invoke(new Action(() => m_ViewModel.UcitajStajalista()));
            });
            task.ContinueWith(t =>
            {
                IsBusy = false;
            });
            task.Start();
        }

        private void OsveziMestaIStajalista()
        {
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.UcitajMesta();
                Invoke(new Action(() => m_ViewModel.UcitajStajalista()));
            });
            task.ContinueWith(t =>
            {
                mestoBindingSource.DataSource = m_ViewModel.Mesta;
                IsBusy = false;
            });
            task.Start();
        }

        
    }
}