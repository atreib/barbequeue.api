namespace barbequeue.api.Data.Protocols
{
    public interface IJwtGenerator
    {
         string GenerateJwt (string jsonData);
    }
}