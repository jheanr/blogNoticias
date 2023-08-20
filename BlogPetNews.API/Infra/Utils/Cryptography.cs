namespace BlogPetNews.API.Infra.Utils
{
    public class Cryptography: ICryptography
    {
        public Cryptography()
        {
        }

        public string Encodes(string password)
        {
            const int WorkFactor = 14;
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        public bool Compares(string password, string hash)
        {
            if (BCrypt.Net.BCrypt.Verify(password, hash) == true) 
                return true;
            return false;
        }
    }
}
