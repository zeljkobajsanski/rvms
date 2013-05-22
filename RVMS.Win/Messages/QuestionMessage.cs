namespace RVMS.Win.Messages
{
    public class QuestionMessage : Message
    { 
        public QuestionMessage(string question) : base(MessageType.Question, question)
        {
        }
    }
}