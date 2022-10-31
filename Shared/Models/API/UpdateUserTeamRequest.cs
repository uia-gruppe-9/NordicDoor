using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Shared.Models.API
{
    public class UpdateUserTeamRequest
    {
        public string Role { get; set; }
    }
}
