namespace Nordic_Door.Shared.Models.API
{
    public class AddSuggestionRequest
    {

        public int TeamId { get; set; }

        public int? ResponsibleEmployee { get; set; }

        public int? ResponsibleTeam { get; set; }

        public int CreatedBy { get; set; } // Må fikse hvordan automatisk loade inn fra database(tror ikke det skal være med set;).

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeadLine { get; set; }

        public DateTime? LastUpdatedAt { get; set; }
        public string? Status { get; set; }

        public string? Phase { get; set; }

        public string? Description { get; set; }
    }
}

