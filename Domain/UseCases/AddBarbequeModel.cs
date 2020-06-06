using System;

namespace barbequeue.api.Domain.UseCases
{
    public class AddBarbequeModel
    {
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
    }
}