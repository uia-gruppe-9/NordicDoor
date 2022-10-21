using System;


namespace Nordic_Door.Shared.Models.API
{
    public class AddUserRequest
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

