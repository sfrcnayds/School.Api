using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace School.API.Helpers
{
    public static class JwtExtansion
    {
        public static void AddApplicationError(this HttpResponse response,string message)
        {
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Allow-Origin","*");
            response.Headers.Add("Access-Control-Expose-Header","Application-Error");
        }
    }
}
