﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RVMS.Win.Annotations;
using Message = RVMS.Win.Messages.Message;

namespace RVMS.Win.Views
{
    public partial class ViewBase : UserControl, INotifyPropertyChanged
    {
        private bool m_IsBusy;

        public ViewBase()
        {
        }

        public bool IsBusy
        {
            get { return m_IsBusy; }
            set
            {
                if (value.Equals(m_IsBusy))
                {
                    return;
                }
                m_IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<RequestViewEventArgs> RequestView;

        public event EventHandler<NotificationEventArgs> Notify;

        protected virtual void OnNotify(Message message)
        {
            var handler = Notify;
            if (handler != null) handler(this, new NotificationEventArgs(){Message = message});
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnRequestView(string viewName, object parameters)
        {
            var h = RequestView;
            if (h != null)
            {
                h(this, new RequestViewEventArgs
                {
                    ViewName = viewName,
                    Parameters = parameters
                });
            }
        }

        public virtual void Osvezi() {}
        
        public virtual void Sacuvaj() {}

        public virtual void NoviUnos() {}

        public virtual void Otvori() {}
    }
}
