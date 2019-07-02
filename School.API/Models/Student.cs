using System;
using System.Collections.Generic;

namespace School.API.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Number { get; set; }
        public string Mail { get; set; }
        public int? DepartmentId { get; set; }
        public string Password { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
