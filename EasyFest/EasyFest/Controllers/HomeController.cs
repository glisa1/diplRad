using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyFest.Models;
using System.Threading.Tasks;
using System.Threading;
using GraphQL.Types;
using GraphQLDataAccess.Schema.Types;
using GraphQLDataAccess.Schema;

namespace EasyFest.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IQuery _query;
        //public HomeController(IQuery query)
        //{
        //    _query = query;
        //}


        public async Task<IActionResult> Index()
        {
            //CancellationToken cancelationToken = new CancellationToken();
            //var festivals = await _query.GetFestivals().ToListAsync(cancelationToken);

            //var inputs = query.Variables.ToInputs();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
