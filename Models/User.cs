namespace RopeyDVD.Models
{
    public enum UserType { Assistant, Manager }

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType UserType { get; set; }

    }
}
