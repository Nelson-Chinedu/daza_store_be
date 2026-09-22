using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace daza_store_be.Dtos.Response
{
    public class SignupResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; } 
    }
}