using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Text;


namespace WebApplication6.Controllers
{
    [Serializable]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBContext _dbcontext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbcontext = new DBContext();
        }

        public IActionResult Index()
        {
            ViewData["slide1_url"] = "https://hips.hearstapps.com/hmg-prod/images/best-bean-to-cup-coffee-machines-1651654175.png";
            ViewData["slide2_url"] = "https://moviebabble.com/wp-content/uploads/2017/07/Dunkirk.jpg";
            ViewData["slide3_url"] = "https://m.media-amazon.com/images/S/aplus-media-library-service-media/b7853a4f-eda1-4532-83ec-329e47a2bfe8.__CR0,0,1464,600_PT0_SX1464_V1___.jpg";

            return View();
        }

        public IActionResult Authenticate(IFormCollection collection)
        {
            string username = collection["username"];
            string password = collection["password"];

            // Uses SHA256 to create the hash
            var sha = new System.Security.Cryptography.SHA256Managed();

            // Convert the string to a byte array first, to be processed
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha.ComputeHash(textBytes);

            // Convert back to a string, removing the '-' that BitConverter adds
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            var sql = "Select * from tblUser where UserEmail = {0} and Password = {1} ";
            UserModel results = _dbcontext.UserModel.SqlQuery(sql, username, hash).FirstOrDefault();

            if (results != null)
            {
                //System.Diagnostics.Debug.WriteLine(results.UserID);

                //set session variables
                HttpContext.Session.SetString("loggedIn", "true");
                HttpContext.Session.SetString("UserID", results.UserID.ToString());
                HttpContext.Session.SetString("FirstName", results.FirstName);


                return View("../Admin/Index");

            }
            else
            {
                //remove all session variables
                HttpContext.Session.Remove("loggedIn");
                HttpContext.Session.Remove("UserID");
                HttpContext.Session.Remove("FirstName");

                ViewData["errorMsg"] = "Invalid Username/Password";
            }

            return View("Login");
        }

        public IActionResult AddToCart(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 1;

            string CartItems = "";
            HttpContext.Session.TryGetValue("Cart_Contents", out var Cart_Contents_InBytes);

            //if there is an existing cart, update it
            if(Cart_Contents_InBytes != null)
            {
                CartItems = Encoding.UTF8.GetString(Cart_Contents_InBytes);

            }
            CartItems = CartItems + id.ToString() + ", ";
            //save the update into a session so that it is not lost
            HttpContext.Session.SetString("Cart_Contents", CartItems);

            System.Diagnostics.Debug.WriteLine("Content is: " + CartItems);
            ViewData["infoMsg"] = "Item Added to Cart Successfully";
            return Redirect("Products");
        }

        public IActionResult AddToTechCart(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 1;

            string CartItems = "";
            HttpContext.Session.TryGetValue("Cart_Contents", out var Cart_Contents_InBytes);

            //if there is an existing cart, update it
            if (Cart_Contents_InBytes != null)
            {
                CartItems = Encoding.UTF8.GetString(Cart_Contents_InBytes);

            }
            CartItems = CartItems + id.ToString() + ", ";
            //save the update into a session so that it is not lost
            HttpContext.Session.SetString("Cart_Contents", CartItems);

            System.Diagnostics.Debug.WriteLine("Content is: " + CartItems);
            ViewData["infoMsg"] = "Item Added to Cart Successfully";
            return Redirect("TechCartView");
        }

        public IActionResult Logout()
        {
            //remove all session variables
            HttpContext.Session.Remove("loggedIn");
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("FirstName");

            ViewData["errorMsg"] = "Logout Succesful";
            return View("Login");
    }


public IActionResult Products(IFormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine("collection is: " + collection);
            if (collection["productname"].ToString().Length != 0)
            {
                string prodname = collection["productname"];
                string productprice = collection["productprice"];
                string productrating = collection["productrating"];
                string productqty = collection["productqty"];
                string productlink = collection["productlink"];
                string productdesc = collection["productdesc"];
                System.Diagnostics.Debug.WriteLine("prod_name is: " + prodname);

                var sql_insert = "insert into tblProduct (ProductName, ProductDescription, ProductPrice, ProductRating, ProductQuantity, ProductImageLink) values ({0}, {1}, {2}, {3}, {4}, {5} )";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, prodname, productdesc, productprice, productrating, productqty, productlink);
            }
            //get all products
            var sql = "Select * from tblProduct";
            List<ProductModel> products = _dbcontext.ProductModel.SqlQuery(sql).ToList();

            ViewData["products"] = products;

            ViewData["slide1_url"] = "https://hips.hearstapps.com/hmg-prod/images/best-bean-to-cup-coffee-machines-1651654175.png";
            ViewData["slide2_url"] = "https://hips.hearstapps.com/hmg-prod/images/best-cheap-vacuum-cleaners-1671118552.png";
            ViewData["slide3_url"] = "https://hips.hearstapps.com/hmg-prod/images/best-oled-tvs-1676283573.png";


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


        public IActionResult Stream(IFormCollection collection)
        {

            System.Diagnostics.Debug.WriteLine("collection is: " + collection);
            if (collection["movietitle"].ToString().Length != 0)
            {
                string movietitle = collection["movietitle"];
                string genre = collection["genre"];
                string synopsis = collection["synopsis"];
                string actorlist = collection["actorlist"];
                string duration = collection["duration"];
                string movielink = collection["movielink"];
                string movietrailerlink = collection["movietrailerlink"];
                System.Diagnostics.Debug.WriteLine("movie_title is: " + movietitle);

                var sql_insert = "insert into tblMovie (MovieTitle, Genre, Synopsis, ActorList, Duration, MovieLink, MovieTrailerLink) values ({0}, {1}, {2}, {3}, {4}, {5}, {6})";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, movietitle, genre, synopsis, actorlist, duration, movielink, movietrailerlink);
            }
            //get all movies
            var sql = "Select * from tblMovie";
            List<MovieModel> movies = _dbcontext.MovieModel.SqlQuery(sql).ToList();
            ViewData["movie"] = movies;


            System.Diagnostics.Debug.WriteLine("collection is: " + collection);
            if (collection["seriestitle"].ToString().Length != 0)
            {
                string seriestitle = collection["seriestitle"];
                string genre = collection["genre"];
                string synopsis = collection["synopsis"];
                string actorlist = collection["actorlist"];
                string episodelist = collection["episodelist"];
                string serieslink = collection["serieslink"];
                string seriestrailerlink = collection["seriestrailerlink"];
                System.Diagnostics.Debug.WriteLine("series_title is: " + seriestitle);

                var sql_insert = "insert into tblSeries (SeriesTitle, Genre, Synopsis, ActorList, EpisodeList, SeriesLink, SeriesTrailerLink) values ({0}, {1}, {2}, {3}, {4}, {5} , {6})";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, seriestitle, genre, synopsis, actorlist, episodelist, serieslink, seriestrailerlink);
            }
            //get all series
            var sql1 = "Select * from tblSeries";
            List<SeriesModel> series = _dbcontext.SeriesModel.SqlQuery(sql1).ToList();
            ViewData["series"] = series;



            ViewData["slide1_url"] = "https://wp.inews.co.uk/wp-content/uploads/2023/01/SEI_139133822.jpg?w=1024";
            ViewData["slide2_url"] = "https://m.media-amazon.com/images/M/MV5BMTQ5MTgyODg0NF5BMl5BanBnXkFtZTgwOTg2MTEwNDE@._V1_.jpg";
            ViewData["slide3_url"] = "https://occ-0-58-358.1.nflxso.net/dnm/api/v6/6gmvu2hxdfnQ55LZZjyzYR4kzGk/AAAABcYkEjFxOjxfKLN2QV76_x0ELE7s5GfKlT6tP7CND07SgE7PDf-l5vYOGTghC-curTgnWZ6bnrBRhEC7QAwCmQ87WBoFPadnaZli.jpg?r=2e9";
            ViewData["slide4_url"] = "https://ichef.bbci.co.uk/images/ic/1200x675/p0dgr4wv.jpg";

            return View();
        }

        public IActionResult Tech(IFormCollection collection)
        {

            System.Diagnostics.Debug.WriteLine("collection is: " + collection);
            if (collection["techname"].ToString().Length != 0)
            {
                string techname = collection["techname"];
                string rentalprice = collection["rentalprice"];
                string supplierid = collection["supplierid"];
                string stock = collection["stock"];
                string techlink = collection["techlink"];
                string rentdesc = collection["rentdesc"];
                System.Diagnostics.Debug.WriteLine("tech_name is: " + techname);

                var sql_insert = "insert into tblRent (ProductName, RentalPrice, SupplierID, Stock, ProductImageLink, Description) values ({0}, {1}, {2}, {3}, {4}, {5} )";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, techname, rentalprice, supplierid, stock, techlink, rentdesc);
            }

            //get all rental tech
            var sql = "Select * from tblRent";
            List<RentModel> rents = _dbcontext.RentModel.SqlQuery(sql).ToList();

            //shows all tech rentals from the database and inserts images for the carousel
            ViewData["rents"] = rents;

            ViewData["slide1_url"] = "https://www.bechtle.com/dam/jcr:cbdd0659-294b-4f5a-9ec2-ead589f9b102/cw33_mainbanner_galaxy-tab-s-series-de.jpg";
            ViewData["slide2_url"] = "https://i0.wp.com/rsgadgets.com/wp-content/uploads/2021/07/Lenovo-Banner.jpg";
            ViewData["slide3_url"] = "https://m.media-amazon.com/images/S/aplus-media-library-service-media/b7853a4f-eda1-4532-83ec-329e47a2bfe8.__CR0,0,1464,600_PT0_SX1464_V1___.jpg";

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

        public IActionResult AddProduct()
        {
            return View();
        }


        public IActionResult AddTech()
   
        {
            return View();
        }
        public IActionResult AddMovie()

        {
            return View();
        }

        public IActionResult Report()
        {
            return View();
        }



        public IActionResult MovieView(IFormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine("collection is: " + collection);
            if (collection["movietitle"].ToString().Length != 0)
            {
                string movietitle = collection["movietitle"];
                string genre = collection["genre"];
                string synopsis = collection["synopsis"];
                string actorlist = collection["actorlist"];
                string duration = collection["duration"];
                string movielink = collection["movielink"];
                System.Diagnostics.Debug.WriteLine("movie_title is: " + movietitle);

                var sql_insert = "insert into tblMovie (MovieTitle, Genre, Synopsis, ActorList, Duration, MovieLink) values ({0}, {1}, {2}, {3}, {4}, {5} )";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, movietitle, genre, synopsis, actorlist, duration, movielink);
            }
            //get all movies
            var sql = "Select * from tblMovie";
            List<MovieModel> movies = _dbcontext.MovieModel.SqlQuery(sql).ToList();

            ViewData["movie"] = movies;

            return View();
        }

        public IActionResult MovieDetails(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 14;
            var sql = "Select * from tblMovie where MovieID = {0}";
            MovieModel movies = _dbcontext.MovieModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["movie"] = movies;
            return View();
        }

        public IActionResult GenreMovieDetails(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 14;
            var sql = "Select * from tblMovieGenre join tblMovie on tblMovie.MovieID = tblMovieGenre.MovieID where tblMovieGenre.GenreID = {0}";
            MovieModel movies = _dbcontext.MovieModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["movie"] = movies;
            return View();
        }


        public IActionResult MovieReview()
        {
            return View();
        }


        public IActionResult Genre(IFormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine("collection is: " + collection);
            if (collection["genretitle"].ToString().Length != 0)
            {
                string genretitle = collection["genretitle"];
                System.Diagnostics.Debug.WriteLine("genre_title is: " + genretitle);

                var sql_insert = "insert into tblMovie (GenreTitle) values ({0})";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, genretitle);
            }
            //get all genre
            var sql = "Select * from tblGenre";
            List<GenreModel> genres = _dbcontext.GenreModel.SqlQuery(sql).ToList();

            ViewData["genre"] = genres;


            return View();
        }

        public IActionResult SeriesView(IFormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine("collection is: " + collection);
            if (collection["seriestitle"].ToString().Length != 0)
            {
                string seriestitle = collection["seriestitle"];
                string genre = collection["genre"];
                string synopsis = collection["synopsis"];
                string actorlist = collection["actorlist"];
                string episodelist = collection["episodelist"];
                string serieslink = collection["serieslink"];
                System.Diagnostics.Debug.WriteLine("series_title is: " + seriestitle);

                var sql_insert = "insert into tblSeries (SeriesTitle, Genre, Synopsis, ActorList, EpisodeList, SeriesLink) values ({0}, {1}, {2}, {3}, {4}, {5} )";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, seriestitle, genre, synopsis, actorlist, episodelist, serieslink);
            }
            //get all series
            var sql = "Select * from tblSeries";
            List<SeriesModel> series1 = _dbcontext.SeriesModel.SqlQuery(sql).ToList();

            ViewData["series"] = series1;

            return View();
        }

        public IActionResult SeriesDetails(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 14;
            var sql = "Select * from tblSeries where SeriesID = {0}";
            SeriesModel series1 = _dbcontext.SeriesModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["series"] = series1;
            return View();
        }

        public IActionResult GenreSeriesDetails(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID is: " + id);
            id = id != 0 ? id : 14;
            var sql = "Select * from tblSeriesGenre join tblSeries on tblSeries.SeriesID = tblSeriesGenre.SeriesID where tblSeriesGenre.GenreID = {0}";
            SeriesModel series1 = _dbcontext.SeriesModel.SqlQuery(sql, id).FirstOrDefault();

            ViewData["series"] = series1;
            return View();
        }

        public IActionResult SeriesReview()
        {
            return View();
        }


        
        public IActionResult CartView()
        {
            HttpContext.Session.TryGetValue("Cart_Contents", out var Cart_Contents_InBytes);
            string CartItems = Encoding.UTF8.GetString(Cart_Contents_InBytes);

            CartItems = "(" + CartItems.Substring(0, CartItems.Length - 2) + ")";
            System.Diagnostics.Debug.WriteLine(CartItems);
            var sql = "Select * from tblProduct where ProductID in "+ CartItems;
            List<ProductModel> products = _dbcontext.ProductModel.SqlQuery(sql).ToList();

            ViewData["products"] = products;
            return View();
        }

        public IActionResult TechCartView()
        {
            HttpContext.Session.TryGetValue("Cart_Contents", out var Cart_Contents_InBytes);
            string CartItems = Encoding.UTF8.GetString(Cart_Contents_InBytes);

            CartItems = "(" + CartItems.Substring(0, CartItems.Length - 2) + ")";
            System.Diagnostics.Debug.WriteLine(CartItems);
            var sql = "Select * from tblRent where RentID in " + CartItems;
            List<RentModel> rents = _dbcontext.RentModel.SqlQuery(sql).ToList();

            ViewData["rents"] = rents;
            return View();
        }

        public IActionResult CheckoutView()
        {
            return View();
        }

        public IActionResult PaymentView()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["slide1_url"] = "https://hips.hearstapps.com/hmg-prod/images/best-bean-to-cup-coffee-machines-1651654175.png";
            ViewData["slide2_url"] = "https://moviebabble.com/wp-content/uploads/2017/07/Dunkirk.jpg";
            ViewData["slide3_url"] = "https://m.media-amazon.com/images/S/aplus-media-library-service-media/b7853a4f-eda1-4532-83ec-329e47a2bfe8.__CR0,0,1464,600_PT0_SX1464_V1___.jpg";
            return View();
        }

        // POST: Home/Authenticate
        
        

        public IActionResult Register(IFormCollection collection)
        {
            Console.WriteLine(collection["username"]);
            if (collection["username"].ToString().Length != 0)
            {

                string username = collection["username"];
                string firstname = collection["firstname"];
                string lastname = collection["lastname"];
                string dateofbirth = collection["dateofbirth"];
                string telephone = collection["telephone"];
                string useremail = collection["useremail"];
                string password = collection["password"];
                string addressline1 = collection["addressline1"];
                string addressline2 = collection["addressline2"];
                string town = collection["town"];
                string postcode = collection["postcode"];

                //hash the password
                // Uses SHA256 to create the hash
                var sha = new System.Security.Cryptography.SHA256Managed();
                
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);


                System.Diagnostics.Debug.WriteLine("user_name is: " + hash);

                var sql_insert = "insert into tblUser (UserName, FirstName, LastName,DateofBirth, UserEmail, Password, AddressLine1, AddressLine2, Town, PostCode, Telephone) values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10} )";
                _dbcontext.Database.ExecuteSqlCommand(sql_insert, username, firstname, lastname, dateofbirth, useremail, hash, addressline1, addressline2, town, postcode, telephone);

                //ViewData["message"] = "Registration Successful. Click here to <a href=''>Login</a>";

            }


            
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
