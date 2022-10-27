namespace Nordic_Door.Shared.Models.API
{
    public class UpdateSuggestionRequest
    {
        public int TeamId { get; set; }

        public int? ResponsibleEmployee { get; set; }

        public int? ResponsibleTeam { get; set; }

        public string Title { get; set; }

        public DateTime? DeadLine { get; set; }

        //public DateTime? LastUpdatedAt { get; } // kan hende denne skal være med.

        public string? Status { get; set; }

        public string? Phase { get; set; }

        public string? Description { get; set; }
    }
}

