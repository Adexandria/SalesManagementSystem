using SalesManagementSystem.Model;
using System.Text;

namespace SalesManagementSystem.Services
{
    public class UserRepository :IUser
    {
        List<User> users = new();
        
        public void CreateUser(User user)
        {
            EncryptPassword(user, user.Password);
            users.Add(user);
        }
        
        public void UpdateUserEmail(Guid userId, string email)
        {
            User user = GetUser(userId);
            if(user is not null)
            {
                user.Email = email;
            }
        }

        public void UpdateUserName(Guid userId, string username)
        {
            User user = GetUser(userId);
            if (user is not null)
            {
                user.Username = username;
            }
        }

        public void UpdateUserPassword(Guid userId, string password)
        {
            User user = GetUser(userId);
            if (user is not null)
            {
                EncryptPassword(user, password);
            }
        }

        public void DeleteUser(Guid userId)
        {
            User user = GetUser(userId);
            if(user is not null)
            {
                users.Remove(user);
            }
        }

        public bool IsUserExist(Guid userId)
        {
            User user = GetUser(userId);
            if(user is null)
            {
                return false;
            }
            return true;
        }

        public bool IsUsernameExist(string username)
        {
            User user = users.FirstOrDefault(u => u.Username == username);
            if (user is null)
            {
                return false;
            }
            return true;
        }
        
        private void EncryptPassword(User user,string password)
        {
            //learn how to encode using utf8
            var encodedPassword = Encoding.UTF8.GetBytes(user.Password).ToString();
            user.Password = encodedPassword;
        }

        private User GetUser(Guid userId)
        {
            return users.FirstOrDefault(u => u.UserId == userId);
        }

    }
}
