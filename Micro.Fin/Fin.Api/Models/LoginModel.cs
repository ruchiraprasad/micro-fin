using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fin.Api.Models
{
    public class LoginModel
    {
        //[Required]
        //[JsonPropertyName("userName")]
        public string UserName { get; set; }
        //[Required]
        //[JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
