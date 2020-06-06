using barbequeue.api.Domain.UseCases;
using barbequeue.api.Domain.Models;
using System.Threading.Tasks;
using barbequeue.api.Data.Protocols;
using System;

namespace barbequeue.api.Data.UseCases
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IEncrypter _encrypter;

        public AuthenticateService (
            IUserRepository userRepository, 
            IJwtGenerator jwtGenerator,
            IEncrypter encrypter
        ) 
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
            _encrypter = encrypter;
        }

        public async Task<User> Authenticate (LoginModel loginData) 
        {
            var user = await _userRepository.GetLogin(loginData.Username);
            if (user == null)
                throw new ApplicationException("Usuário incorreto");

            bool isPasswordCorrect = _encrypter.compare(user.Password, loginData.Password);
            if (!isPasswordCorrect)
                throw new ApplicationException("Senha incorreta");

            user.Token = _jwtGenerator.GenerateJwt(loginData.Username);
            return user;
        }
    }
}