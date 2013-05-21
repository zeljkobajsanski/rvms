using System;
using RVMS.Win.Messages;

namespace RVMS.Win
{
    public class NotificationEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}