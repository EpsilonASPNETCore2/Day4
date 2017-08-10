using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EComm.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Empty")]
    public class EmptyController : Controller
    {
    }
}