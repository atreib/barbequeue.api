using System.Threading.Tasks;

namespace barbequeue.api.Data.Protocols
{
    public interface IEncrypter
    {
        string hash (string value);
        bool compare (string hashedPassword, string providedPassword);
    }
}