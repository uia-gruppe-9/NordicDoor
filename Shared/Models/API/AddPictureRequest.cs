using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.API
{
    public class AddPictureRequest
    {

        public int SuggestionId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime UploadedAt { get; set; }

        public byte[] Image { get; set; }

        
    }
}
