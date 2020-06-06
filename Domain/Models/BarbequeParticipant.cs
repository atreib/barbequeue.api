namespace barbequeue.api.Domain.Models
{
    public class BarbequeParticipant
    {
        public BarbequeParticipant () { }
        public BarbequeParticipant (int bbqId, string name, decimal contribution, bool paid)
        {
            this.BarbequeId = bbqId;
            this.Name = name;
            this.Contribution = contribution;
            this.Paid = paid;
        }

        public int Id { get; set; }
        public int BarbequeId { get; set; }
        public string Name { get; set; }
        public decimal Contribution { get; set; }
        public bool Paid { get; set; }
        
        public Barbeque Barbeque { get; set; }
    }
}