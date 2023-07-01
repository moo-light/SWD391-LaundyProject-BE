using System.Security.Cryptography;

namespace Application.Utils
{
    public static class RefreshTokenString
    {
        public static string GetRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);

        }

    }
}
