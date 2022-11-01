using Nordic_Door.Shared.Models.Database;
namespace Nordic_Door.Shared.Models.API
{
    public class GetSuggestionRequest
    {
        // Constructor er trengs siden i ViewSuggestions så må vi lage flere GetSuggestionsRequest, og de kan ikke ha samme referanse
        // Denne lar oss lage en ny request fra en annen eksisterende request litt lettere.
        public GetSuggestionRequest(GetSuggestionRequest request)
        {
            Id = request.Id;
            Team = request.Team;
            ResponsibleEmployee = request.ResponsibleEmployee;
            ResponsibleTeam = request.ResponsibleTeam;
            CreatedBy = request.CreatedBy;
            Title = request.Title;
            CreatedAt = request.CreatedAt;
            DeadLine = request.DeadLine;
            LastUpdatedAt = request.LastUpdatedAt;
            Status = request.Status;
            Phase = request.Phase;
            Description = request.Description;
        }

        // Default constructor
        public GetSuggestionRequest () {}

        public int Id { get; set; }

        public Team Team { get; set; }

        public Employee? ResponsibleEmployee { get; set; }

        public Team? ResponsibleTeam { get; set; }

        public Employee CreatedBy { get; set; } // Må fikse hvordan automatisk loade inn fra database(tror ikke det skal være med set;).

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeadLine { get; set; }

        public DateTime? LastUpdatedAt { get; set; } // gir null verdig atm, kan hende det skal være med en set.

        public string? Status { get; set; }

        public string? Phase { get; set; }

        public string? Description { get; set; }
    }
}

