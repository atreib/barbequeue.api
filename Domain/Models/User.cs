using System.ComponentModel.DataAnnotations.Schema;

namespace barbequeue.api.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set;}
        
        [NotMapped]
        public string Token { get; set; }
    }
}