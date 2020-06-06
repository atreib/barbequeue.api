using barbequeue.api.Data.Protocols;
using System;

namespace barbequeue.api.Helpers.Encrypter
{
    public class Encrypter : IEncrypter
    {
        private readonly string _salt;

        public Encrypter () {
            _salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public string hash (string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value, _salt);
        }

        public bool compare (string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null) { throw new ArgumentNullException(nameof(hashedPassword)); }
            if (providedPassword == null) { throw new ArgumentNullException(nameof(providedPassword)); }
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}