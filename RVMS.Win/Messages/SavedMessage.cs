namespace RVMS.Win.Messages
{
    public class SavedMessage : Message
    {
         public SavedMessage() : base(MessageType.Ok, "Podaci su uspešno sačuvani")
         {
         }
    }
}