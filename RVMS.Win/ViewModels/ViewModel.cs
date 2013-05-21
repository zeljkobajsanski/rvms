using System.ComponentModel;
using System.Runtime.CompilerServices;
using RVMS.Win.Annotations;

namespace RVMS.Win.ViewModels
{
    public class ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void Init() {}

        public virtual string this[string columnName]
        {
            get { return null; }
        }

        public virtual string Error { get; protected set; }

        public virtual bool IsValid { get { return true; } }
    }
}