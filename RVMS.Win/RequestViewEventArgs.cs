using System;
using RVMS.Win.Views;

namespace RVMS.Win
{
    public class RequestViewEventArgs : EventArgs
    {
        public string ViewName { get; set; }
        public object Parameters { get; set; }
    }
}