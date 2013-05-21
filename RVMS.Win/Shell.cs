using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using RVMS.Win.Views;


namespace RVMS.Win
{
    public partial class Shell : RibbonForm
    {
        private BaseDocument daljinar;
        public BaseDocument relacija { get; set; }
        public Shell()
        {
            InitializeComponent();
            InitSkinGallery();
            navBarItemRelacija.LinkClicked += (s, e) =>
            {
                if (relacija == null)
                {
                    relacija = documentManager1.View.AddDocument("Relacija", "ViewRelacija");
                }
                documentManager1.View.ActivateDocument(relacija.Control);
                
            };
            navBarItemDaljinar.LinkClicked += (s, e) =>
            {
                if (daljinar == null)
                {
                    daljinar = documentManager1.View.AddDocument("Daljinar", "ViewDaljinar");
                }
                documentManager1.View.ActivateDocument(daljinar.Control);
            };
            repositoryItemMarqueeProgressBar1.Stopped = true;
        }

        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            switch (e.Document.ControlName)
            {
                case "ViewRelacija":
                    var view = new ViewRelacija();
                    view.PropertyChanged += ViewPropertyChanged;
                    view.RequestView += (s, e1) =>
                    {
                        
                    };
                    e.Control = view;
                    break;
                case "ViewDaljinar":
                    e.Control = new ViewDaljinar();
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
                }));
            }
        }

    }
}