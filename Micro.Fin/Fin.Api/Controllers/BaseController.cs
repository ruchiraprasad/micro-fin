using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string Username { get; set; } 
        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            if(httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User != null)
            {
                var userContext = httpContextAccessor.HttpContext.User;
                this.Username = userContext.FindFirst("username")?.Value;
            }
        }
    }
}