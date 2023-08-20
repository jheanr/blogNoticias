namespace BlogPetNews.API.Infra.Utils
{
    public interface ICryptography
    {
        public string Encodes(string password);
        public bool Compares(string password, string hash);



    }
}
