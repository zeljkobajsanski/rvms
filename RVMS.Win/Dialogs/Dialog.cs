using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RVMS.Win.Dialogs
{
    public class Dialog : XtraForm
    {
         protected void ShowError(Exception exc)
         {
             XtraMessageBox.Show(this, exc.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
    }
}