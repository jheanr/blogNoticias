namespace BlogPetNews.API.Domain.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll(int page, int take);
        User GetById(Guid id);
        User Create(User user);
        User Update(User user);
        void Delete(Guid id);
        User Login(string email, string password);
    }
}
