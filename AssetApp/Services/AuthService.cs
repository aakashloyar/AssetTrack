using AssetApp.Models;
using Microsoft.Extensions.Configuration;

namespace AssetApp.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateUser(LoginModel loginModel)
        {
            var userConfig = _configuration.GetSection("SingleUser");
            string configuredUsername = userConfig["Username"];
            string configuredPassword = userConfig["Password"];

            return loginModel.Username == configuredUsername && loginModel.Password == configuredPassword;
        }
    }
}
