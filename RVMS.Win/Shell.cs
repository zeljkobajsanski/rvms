using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using Korisnici.ClientLibrary;
using RVMS.Win.Dialogs;
using RVMS.Win.Messages;
using RVMS.Win.Views;


namespace RVMS.Win
{
    public partial class Shell : RibbonForm
    {
        public Shell()
        {
            InitializeComponent();
            InitSkinGallery();
            InitCommands();
            navBarItemRelacija.LinkClicked += (s, e) => AddDocumentRelacija(null);
            navBarItemDaljinar.LinkClicked += (s, e) => AddDocumentDaljinar();
            iStajalista.LinkClicked += (s, e) => AddDocumentStajalista();
            repositoryItemMarqueeProgressBar1.Stopped = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                siStatus.Caption = "Build: " + ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            using (var login = new Login())
            {
                var result = login.ShowDialog(this);
                if (DialogResult.OK != result) Application.Exit();
                WindowState = FormWindowState.Maximized;
                iUser.Caption = ApplicationContext.Current.ImeIPrezime;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            new Account(ApplicationContext.Current.LoginService).Logout(ApplicationContext.Current.LogId);
        }

        private void AddDocumentStajalista()
        {
            var view = new ViewStajalista();
            view.PropertyChanged += ViewPropertyChanged;
            view.Notify += OnNotify;
            view.RequestView += (s1, e1) => AddDocumentRelacija(e1.Parameters);
            var doc = documentManager1.View.Controller.AddDocument(view);
            doc.Caption = "Stajališta";
        }

        private void AddDocumentDaljinar()
        {
            var view = new ViewDaljinar();
            view.PropertyChanged += ViewPropertyChanged;
            view.Notify += OnNotify;
            view.RequestView += (s1, e1) => AddDocumentRelacija(e1.Parameters);
            var doc = documentManager1.View.Controller.AddDocument(view);
            doc.Caption = "Daljinar";
        }

        private void AddDocumentRelacija(object parameters)
        {
            var view = parameters != null ? new ViewRelacija(parameters) : new ViewRelacija();
            view.PropertyChanged += ViewPropertyChanged;
            view.Notify += OnNotify;
            var doc = documentManager1.View.Controller.AddDocument(view);
            doc.Caption = "Relacija";
        }

        private void InitCommands()
        {

            iRefresh.ItemClick += (s, e) =>
            {
                var active = documentManager1.View.ActiveDocument;
                if (active == null) return;
                var view = (ViewBase)active.Control;
                view.Osvezi();
            };
            iSave.ItemClick += (s, e) =>
            {
                var active = documentManager1.View.ActiveDocument;
                if (active == null) return;
                var view = (ViewBase)active.Control;
                view.Sacuvaj();
            };
            iNew.ItemClick += (s, e) =>
            {
                var active = documentManager1.View.ActiveDocument;
                if (active == null) return;
                var view = (ViewBase)active.Control;
                view.NoviUnos();
            };
            iExit.ItemClick += (s, e) =>
            {
                if (Pitaj("Da li želite da napustite aplikaciju?"))
                {
                    Close();
                }
            };
            iMojNalog.ItemClick += (s, e) =>
            {
                var view = new ViewKorisnickiNalog();
                view.PropertyChanged += ViewPropertyChanged;
                view.Notify += OnNotify;
                var doc = documentManager1.View.Controller.AddDocument(view);
                doc.Caption = "Korisnički nalog";
            };
            iPassword.ItemClick += (s, e) =>
            {
                var view = new ViewPromenaLozinke();
                view.PropertyChanged += ViewPropertyChanged;
                view.Notify += OnNotify;
                var doc = documentManager1.View.Controller.AddDocument(view);
                doc.Caption = "Promena lozinke";
            };
        }

        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
        }

        private void OnNotify(object sender, NotificationEventArgs e)
        {
            switch (e.Message.MessageType)
            {
                    case MessageType.Ok:
                        //XtraMessageBox.Show(this, e.Message.MessageText, "Oktopod", MessageBoxButtons.OK,
                        //                    MessageBoxIcon.Information);
                        alertControl1.Show(this, "Info", e.Message.MessageText, Properties.Resources.info_32x32);
                        break;
                    case MessageType.Warning:
                        //XtraMessageBox.Show(this, e.Message.MessageText, "Oktopod", MessageBoxButtons.OK,
                        //                    MessageBoxIcon.Warning);
                        alertControl1.Show(this, "Upozorenje", e.Message.MessageText, Properties.Resources.warning_32x32);
                        break;
                    case MessageType.Error:
                        //XtraMessageBox.Show(this, e.Message.MessageText, "Oktopod", MessageBoxButtons.OK,
                        //                        MessageBoxIcon.Error);
                        alertControl1.Show(this, "Greška", e.Message.MessageText, Properties.Resources.error_32x32);
                        break;
                    case MessageType.Question:
                        e.Message.Confirm = Pitaj(e.Message.MessageText);
                        break;
            }
        }

        private void ViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBusy")
            {
                var view = (ViewBase)sender;
                Invoke(new Action(() =>
                {
                    repositoryItemMarqueeProgressBar1.Stopped = !view.IsBusy;
                    //Cursor = view.IsBusy ? Cursors.WaitCursor : Cursors.Default;
                }));
            }
        }

        private bool Pitaj(string pitanje)
        {
            return
                XtraMessageBox.Show(this, pitanje, "Oktopod", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

    }
}