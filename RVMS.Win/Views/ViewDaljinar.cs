using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Views
{
    public partial class ViewDaljinar : ViewBase
    {
        private readonly DaljinarViewModel m_ViewModel = new DaljinarViewModel();

        public ViewDaljinar()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var task = new Task(() =>
            {
                IsBusy = true;
                m_ViewModel.UcitajDaljinar();
            });
            task.ContinueWith((t) =>
            {
                Invoke(new Action(() =>
                {
                    relacijaDTOBindingSource.DataSource = m_ViewModel.Daljinar;    
                }));
                IsBusy = false;
            });
            task.Start();
        }
    }
}
