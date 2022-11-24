using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Shared.Models.API
{
    public class GetTeamStatisticsByDateRange
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }
}