namespace barbequeue.api.Domain.UseCases
{
    public class AddBarbequeParticipantModel
    {
        public int BarbequeId { get; set; }
        public string Name { get; set; }
        public decimal Contribution { get; set; }
        public bool Paid { get; set; }
    }
}