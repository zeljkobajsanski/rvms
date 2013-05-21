namespace RVMS.Win.Messages
{
    public class InvalidForSaveMessage : Message
    {
        public InvalidForSaveMessage() : base(MessageType.Warning, "Ispravite sve greške pre snimanja")
        {
        }
    }
}