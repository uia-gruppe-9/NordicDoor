using System;
namespace Nordic_Door.Shared.Models.API
{
    public class UpdateSuggestionRequest
    {
        public int TeamId { get; set; }

        public int? ResponsibleEmployee { get; set; }

        public int? ResponsibleTeam { get; set; }

        public string Title { get; set; }       

        public DateTime? DeadLine { get; set; }

        public string? Phase { get; set; }

        public string? Description { get; set; }
    }
}

