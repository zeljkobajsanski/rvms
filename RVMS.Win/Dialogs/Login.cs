using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Dialogs
{
    public partial class Login : XtraForm
    {

        private readonly LoginViewModel fViewModel;

        public Login()
        {
            InitializeComponent();
            fViewModel = new LoginViewModel();
            loginViewModelBindingSource.DataSource = fViewModel;
            textEdit1.Focus();
            HandleEvents();
        }

        private void HandleEvents()
        {
            simpleButton1.Click += (sender, args) => Prijavi();
            simpleButton2.Click += (sender, args) => Application.Exit();
            textEdit2.KeyDown += (s, e) =>
                                     {
                                         if (e.KeyCode == Keys.Enter)
                                         {
                                             Prijavi();
                                         }
                                     };
        }

        private void Prijavi()
        {
            var ok = fViewModel.PrijaviMe();
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        
    }
}
