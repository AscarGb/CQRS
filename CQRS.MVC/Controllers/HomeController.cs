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

namespace CQRS.MVC.Controllers
{
    public class HomeController : Controller
    {
        UserCommandHandlerFactory _userCommandHandlerFactory;
        UserQueryHandlerFactory _userQueryHandlerFactory;

        public HomeController(UserQueryHandlerFactory userQueryHandlerFactory,
            UserCommandHandlerFactory userCommandHandlerFactory)
        {
            _userQueryHandlerFactory = userQueryHandlerFactory;
            _userCommandHandlerFactory = userCommandHandlerFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new AllUsersQuery();
            var handler = _userQueryHandlerFactory.Build(query);
            var users = await handler.GetAsync();
            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string name)
        {
            var query = new OneUserQuery { Name = name };
            var handler = _userQueryHandlerFactory.Build(query);
            var user = await handler.GetAsync();

            if (user == null)
                return NotFound(name);
            else
                return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string name, string password)
        {
            var command = new SaveUserCommand(new User { Name = name }, password);
            var handler = _userCommandHandlerFactory.Build(command);
            var cResponse = await handler.ExecuteAsync();

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
