using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.XtraEditors.ColorPick.Picker;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Linq;

namespace RVMS.Win.Models
{
    public class ClientModel : IDXDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void GetPropertyError(string propertyName, ErrorInfo info)
        {
        }

        public virtual void GetError(ErrorInfo info)
        {
        }
    }
}