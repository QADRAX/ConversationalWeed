namespace ConversationalWeed.Models
{
    public class PlayCardRequest
    {
        public ulong PlayerId { get; set; }
        public ulong TargetPlayerId { get; set; }
        public int TagetPlayerFieldId { get; set; }
        public CardType CardType { get; set; }
        public int BeneficiaryPlayerFieldId { get; set; }

    }
}
