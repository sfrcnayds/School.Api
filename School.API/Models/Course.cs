using System;
using System.Collections.Generic;

namespace School.API.Models
{
    public partial class Course
    {
        public Course()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
