using System;
using System.Collections.Generic;

namespace School.API.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Course = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public string Password { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
