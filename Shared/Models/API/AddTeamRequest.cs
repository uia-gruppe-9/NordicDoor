using System;
namespace Nordic_Door.Shared.Models.API
{
    public class AddTeamRequest
    {
        public int Id { get; }
        public int LeaderId { get; set; }
        public string Name { get; set; }
    }
}

