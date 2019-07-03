using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Dtos
{
    public class AddMarkDto
    {
        public int CourseStudentId { get; set; }
        public int Mark { get; set; }
    }
}
