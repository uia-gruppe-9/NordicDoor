using Nordic_Door.Shared.Models.Database;
using System;


namespace Nordic_Door.Shared.Models.API
{
    public class GetEventRequest
    {
    public int Id { get; set; }
    public Employee Employee { get; set; }
   
    public Suggestion Suggestion { get; set; }

    public string? Description { get; set; }

    public DateTime Timestamp { get; set; }

    }
}