using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace WebApplication6.Models
{
    public class DBContext : DbContext
    {
        const string connURL = "Data Source=teamphoenix123.database.windows.net;Initial Catalog=teamphoenixDB;User ID=CloudSAc3a3c3fe;Password=Database345!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DBContext() : base(connURL)
        {
            
        }

        public DbSet<UserModel> UserModel { get; set; }

        public DbSet<ProductModel> ProductModel { get; set; }

        public DbSet<RentModel> RentModel { get; set; }

        public DbSet<RentedModel> RentedModel { get; set; }

        public DbSet<SupplierModel> SupplierModel { get; set; }

        public DbSet<MovieModel> MovieModel { get; set; }

        public DbSet<SeriesModel> SeriesModel { get; set; }

        public DbSet<GenreModel> GenreModel { get; set; }

        public DbSet<MovieGenreModel> MovieGenreModel { get; set; }

        public DbSet<SeriesGenreModel> SeriesGenreModel { get; set; }

        public DbSet<AdminModel> AdminModel { get; set; }
    }
}
