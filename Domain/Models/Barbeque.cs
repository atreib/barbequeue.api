using System.Collections.Generic;
using System;

namespace barbequeue.api.Domain.Models
{
    public class Barbeque
    {
        public Barbeque() { }
        public Barbeque (string desc, DateTime eventDateTime) 
        {
            this.Description = desc;
            this.EventDateTime = eventDateTime;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }

        public ICollection<BarbequeParticipant> Participants { get; set; }
    }
}