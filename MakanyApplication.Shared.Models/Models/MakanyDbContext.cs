using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakanyApplication.Shared.Models.Models
{
    public class MakanyApplicationDbContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Government> Government { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
        public DbSet<UserRate> UserRate { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL(new MySqlConnectionStringBuilder
            //{
            //    Server = "localhost",
            //    Port = 3306,
            //    UserID = "root",
            //    Password = "Ahmed!994",
            //    Database = "MakanyApplication"
            //}.ConnectionString);
            //base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(new SqlConnectionStringBuilder
            {
                //DataSource = @".",
                DataSource = @".",
                InitialCatalog = "MakanyApplication",
                IntegratedSecurity = true
            }.ConnectionString);
        }
    }
}
