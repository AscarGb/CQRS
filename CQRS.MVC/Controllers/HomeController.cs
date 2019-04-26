using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CQRS.MVC.Models;
using CQRS.Domain.Queries.Handler;
using CQRS.Domain.Queries;
using CQRS.Domain.Commands;
using CQRS.Domain.Queries.Query;
using System.Threading.Tasks;
using CQRS.Data;
using System.Collections.Generic;
using CQRS.Domain.Commands.Command;
using CQRS.Domain.Pipelines;
using CQRS.Domain;

namespace CQRS.MVC.Controllers
{
    public class HomeController : Controller
    {
        Messages _messages;

        CreateUserPipelineBuilder _createUserPipelineBuilder;

        public HomeController(CreateUserPipelineBuilder createUserPipelineBuilder,
            Messages messages)
        {           
            _createUserPipelineBuilder = createUserPipelineBuilder;
            _messages = messages;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new AllUsersQuery();
            var users = await _messages.Dispatch(query);            
            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string name)
        {
            var query = new OneUserQuery { Name = name };            
            var user = await _messages.Dispatch(query);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string name, string password)
        {
            var command = new SaveUserCommand(new User { Name = name }, password);

            command = await _createUserPipelineBuilder.ProcessAsync(command);            

            var cResponse = await _messages.Dispatch(command);
            return Ok(cResponse);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
