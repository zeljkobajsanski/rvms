namespace RVMS.Win.Messages
{
    public class Message
    {
        public MessageType MessageType { get; set; }
        public string MessageText { get; set; }
        public bool Confirm { get; set; }

        public Message(MessageType messageType)
        {
            MessageType = messageType;
        }

        public Message(MessageType messageType, string messageText)
        {
            MessageType = messageType;
            MessageText = messageText;
        }
    }
}