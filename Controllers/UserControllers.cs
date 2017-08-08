using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace quotingDojo.Controllers
{
    public class UserController : Controller
    {
        
        public UserController(){
            
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/quotes/new")]
        public IActionResult Create(string name, string content)
        {
            // System.Console.WriteLine($"{name} {content}");
            string query= $"INSERT INTO users (name,createdAt,updatedAt) VALUES ('{name}',Now(),Now());";
            DbConnector.Execute(query);
            var user= DbConnector.Query("SELECT * FROM users ORDER BY id DESC LIMIT 1;");
            System.Console.WriteLine($"the query result for user {user[0]["id"]}");
            int id=(int)user[0]["id"];
            query=$"INSERT INTO quotes (user_id,content,createdAt,updateAt) VALUES ('{id}','{content}',Now(),Now());";
            DbConnector.Execute(query);
            // var quotes= DbConnector.Query("SELECT users.id as id, name as name, content,quotes.createdAt as createdAt, quotes.id as quote_id FROM users INNER JOIN quotes on quotes.user_id=users.id order by quotes.createdAt Desc;");
            // ViewBag.quotes= quotes;
            // System.Console.WriteLine("SDGDGFDGDGD&*********************{0}",quotes[0]["name"]);
            return RedirectToAction("Show");
        }


        [HttpGet]
        [Route("/quotes")]
        public IActionResult Show()
        {
            //get quotes
            var quotes= DbConnector.Query("SELECT users.id as id, name as name, content,quotes.createdAt as createdAt, quotes.id as quote_id FROM users INNER JOIN quotes on quotes.user_id=users.id order by quotes.createdAt Desc;");
            ViewBag.quotes= quotes;
            // System.Console.WriteLine("SDGDGFDGDGD&*********************{0}",quotes[0]["name"]);
            return View("show");

        }
    }
}
