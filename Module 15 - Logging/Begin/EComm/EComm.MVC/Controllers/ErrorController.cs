using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/{statusCode:int}")]
        public IActionResult Index(int statusCode)
        {
            ViewBag.StatusCode = statusCode;
            return View("Error");
        }
    }
}
