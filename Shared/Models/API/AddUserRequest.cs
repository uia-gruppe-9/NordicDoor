using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Shared.Models.API
{
    public class AddUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> TeamNames{ get;  set; }
        public bool IsAdmin { get; set; }
        
    }
}

