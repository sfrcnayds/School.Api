using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Dtos
{
    public class StudentForCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Number { get; set; }
        public string Mail { get; set; }
        public int? DepartmentId { get; set; }
        public string Password { get; set; }
    }
}
