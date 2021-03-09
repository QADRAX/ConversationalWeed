namespace ConversationalWeed.Models
{
    public class DiscardCardRequest
    {
        public ulong PlayerId { get; set; }
        public CardType CardType { get; set; }
    }
}
