using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public interface IUser
    {
        bool IsUserExist(Guid userId);
        bool IsUsernameExist(string username);
        void CreateUser(User user);
        void UpdateUserEmail(Guid userId, string email);
        void UpdateUserPassword(Guid userId, string password);
        void UpdateUserName(Guid userId, string username);
        void DeleteUser(Guid userId);
    }
}
