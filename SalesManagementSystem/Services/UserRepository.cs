using SalesManagementSystem.Model;
using System.Text;

namespace SalesManagementSystem.Services
{
    public class UserRepository :IUser
    {
        List<User> users = new();
        
        //create user
        public Guid CreateUser(User user)
        {
            user.UserId = Guid.NewGuid();
            EncryptPassword(user, user.Password);
            users.Add(user);
            return user.UserId;
        }
        
        //update user email
        public void UpdateUserEmail(Guid userId, string email)
        {
            User user = GetUser(userId);
            if(user is not null)
            {
                user.Email = email;
            }
        }

        //update user name
        public void UpdateUserName(Guid userId, string username)
        {
            User user = GetUser(userId);
            if (user is not null)
            {
                user.Username = username;
            }
        }

        //update user password
        public void UpdateUserPassword(Guid userId, string password)
        {
            User user = GetUser(userId);
            if (user is not null)
            {
                EncryptPassword(user, password);
            }
        }

        //Check if user exist

        public bool IsUserExist(Guid userId)
        {
            User user = GetUser(userId);
            if (user is null)
            {
                return false;
            }
            return true;
        }
        
        //Check if username exist

        public bool IsUsernameExist(string username)
        {
            User user = users.FirstOrDefault(u => u.Username == username);
            if (user is null)
            {
                return false;
            }
            return true;
        }

        //Delete user
        public void DeleteUser(Guid userId)
        {
            User user = GetUser(userId);
            if(user is not null)
            {
                users.Remove(user);
            }
        }

      

        //Encrypt password
        
        private void EncryptPassword(User user,string password)
        {
            byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
            user.Password = Convert.ToBase64String(encodedPassword);
        }

        
        //Get user
        private User GetUser(Guid userId)
        {
            return users.FirstOrDefault(u => u.UserId == userId);
        }

    }
}
