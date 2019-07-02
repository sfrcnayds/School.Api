using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Dtos
{
    public class SelectCourseDto
    {
        public int studentId { get; set; }
        public int courseId { get; set; }
    }
}
