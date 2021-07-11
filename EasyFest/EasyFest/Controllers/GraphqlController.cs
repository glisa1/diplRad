using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.AspNetCore.Mvc;

namespace EasyFest.Controllers
{
    public class GraphqlController : Controller
    {
        [Route("graphql")]
        public IActionResult Index([FromBody] string BodyRequest)
        {
            Schema schema = new Schema();

            return View();
        }
    }
}