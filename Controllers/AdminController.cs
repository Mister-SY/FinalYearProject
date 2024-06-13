using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly DBContext _dbcontext;
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
            _dbcontext = new DBContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {

            return View();
        }

        public IActionResult ProductEdit()
        {

            return View();
        }

            public IActionResult ProductManager()
        {
            var sql = "Select * from tblProduct";
            List<ProductModel> products = _dbcontext.ProductModel.SqlQuery(sql).ToList();

            ViewData["products"] = products;
            return View();
        }

        public IActionResult CustomerManager()
        {
            return View();
        }
        public IActionResult ProductView(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 14;
            var sql = "Select * from tblProduct where ProductID = {0}";
            ProductModel product = _dbcontext.ProductModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["product"] = product;
            return View();
        }

       public IActionResult ProductUpdate(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 14;
            var sql = "Select * from tblProduct where ProductID = {0}";
            ProductModel product = _dbcontext.ProductModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["product"] = product;
            return View();
        }



        public IActionResult StreamMovieManager()

        {
            var sql = "Select * from tblMovie";
            List<MovieModel> movie = _dbcontext.MovieModel.SqlQuery(sql).ToList();

            ViewData["movie"] = movie;
            return View();
        }

        public IActionResult StreamSeriesManager()
        {
            var sql = "Select * from tblSeries";
            List<SeriesModel> series = _dbcontext.SeriesModel.SqlQuery(sql).ToList();

            ViewData["series"] = series;
            return View();
        }


        public IActionResult Tech()
        {
            return View();
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        public IActionResult AddSeries()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult TechManager()
        {
            var sql = "Select * from tblRent";
            List<RentModel> rents = _dbcontext.RentModel.SqlQuery(sql).ToList();

            ViewData["rents"] = rents;
            return View();
        }

        public IActionResult TechView(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 1;
            var sql = "Select * from tblRent where RentID = {0}";
            RentModel rent = _dbcontext.RentModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["rent"] = rent;
            return View();
        }

        public IActionResult TechUpdate(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 1;
            var sql = "Select * from tblRent where RentID = {0}";
            RentModel rent = _dbcontext.RentModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["rent"] = rent;
            return View();
        }

        public IActionResult AddTech()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: Home/Authenticate
        [HttpPost]
        public IActionResult Authenticate(Microsoft.AspNetCore.Http.IFormCollection collection)
        {
            string username = collection["password"];
            string password = collection["password"];

            /*var authCheck = ctx.Students.SqlQuery("Select studentid as id, studentname as name from Student where studentname='New Student1'").ToList();

            if (authCheck)
            {
                return View("Admin");
            }*/
            return View("Login");
            

        }
        // POST: Home/Register
        [HttpPost]
        public IActionResult Register()
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
