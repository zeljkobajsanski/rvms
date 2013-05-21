using System;

namespace RVMS.Win.Messages
{
    public class ErrorMessage : Message
    {
        public ErrorMessage(Exception exc) : base(MessageType.Error)
        {
            MessageText = exc.Message;
        }
    }
}