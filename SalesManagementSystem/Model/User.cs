namespace SalesManagementSystem.Model
{
    public class User
    {
        public User()
        {
            UserId = Guid.NewGuid();
        }
       public Guid UserId { get; set; }
       public string Username { get; set; }
       public string Email { get; set; }
       public string Password { get; set; }
        public List<Order> Orders { get; set; } = new();
       
    }
}
