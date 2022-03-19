using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Security
{
    public class SecurityKeyGenerator
    {
        private static SecurityKeyGenerator instance = null;
        private static SymmetricSecurityKey key = null;

        public static SecurityKeyGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecurityKeyGenerator();
                }
                return instance;
            }
        }

        public SymmetricSecurityKey GetKey()
        {
            return key;
        }

        private SecurityKeyGenerator()
        {
            TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
            tripleDes.GenerateKey();
            key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(tripleDes.Key.ToString()));
        }
    }
}