using System;
using System.Collections.Generic;

namespace School.API.Models
{
    public partial class Department
    {
        public Department()
        {
            Course = new HashSet<Course>();
            Student = new HashSet<Student>();
            Teacher = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
