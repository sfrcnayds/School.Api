using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Dtos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}
