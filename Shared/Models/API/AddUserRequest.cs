using System;


namespace Nordic_Door.Shared.Models.API
{
    public class AddUserRequest
    {
        public int Id { get; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}

