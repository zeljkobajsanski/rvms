using System;
using System.Windows.Forms;

namespace RVMS.Win.Dialogs
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
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.InvokeScript(script, parameters);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}