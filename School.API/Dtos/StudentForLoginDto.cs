using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Dtos
{
    public class StudentForLoginDto
    {
        public long number { get; set; }
        public string password { get; set; }
    }
}
