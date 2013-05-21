using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RVMS.Win.Messages;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewRelacija : ViewBase
    {
        private readonly RelacijaViewModel m_ViewModel;

        public ViewRelacija()
        {
            InitializeComponent();
            m_ViewModel = new RelacijaViewModel();
            m_ViewModel.PropertyChanged += ModelPropertyChanged;
            relacijaViewModelBindingSource.DataSource = m_ViewModel;
            Enable(false);
            simpleButton1.Click += (s, e) => Dodaj();
        }

        private void Dodaj()
        {
            m_ViewModel.Dodaj();
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
                opstinaBindingSource1.DataSource = m_ViewModel.Opstine;
                IsBusy = false;
            });
            task.Start();
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
                txtRazdaljina.SelectAll();
                txtRazdaljina.Focus();
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
            lookUpEdit1.Enabled = enable;
            lookUpEdit2.Enabled = enable;
            lookUpEdit3.Enabled = enable;
            lookUpEdit4.Enabled = enable;
            txtRazdaljina.Enabled = enable;
            txtVremeVoznje.Enabled = enable;
            simpleButton1.Enabled = enable;
            gridView1.OptionsBehavior.Editable = enable;
            
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
        }
    }
}
