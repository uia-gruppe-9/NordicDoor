using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.API
{
    public class AddSuggestionRequest
    {
        public int Id { get; }

        public int TeamId { get; set; }

        public int? ResponsibleEmployee { get; set; }

        public int? ResponsibleTeam { get; set; }

        public int CreatedBy { get; set; } // Må fikse hvordan automatisk loade inn fra database(tror ikke det skal være med set;).

        public string Title { get; set; }

        public DateTime CreatedAt { get; }

        public DateTime? DeadLine { get; set; }

        public DateTime? LastUpdatedAt { get; }

        public string? Phase { get; set; }

        public string? Description { get; set; }
    }
}

