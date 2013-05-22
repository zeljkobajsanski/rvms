using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RVMS.Win.Views
{
    public partial class Mapa : DevExpress.XtraEditors.XtraForm
    {
        public Mapa()
        {
            InitializeComponent();
        }

        public Mapa(string htmlFile) : this()
        {
            webBrowser1.Url = new Uri(htmlFile);

        }

        public void InvokeScript(string script, params object[] parameters)
        {
            webBrowser1.Document.InvokeScript(script, parameters);
        }
    }
}