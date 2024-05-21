using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACarProject.Models;

namespace RentACarProject.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            
        }
        
        //Oluşacak olan veritabanı şemasını context sınıfında tanımlarız
        public DbSet<Car>? Cars { get; set; }//burdaki Car class ımız veritabanında Cars tablosuna karşılık gelcek
        public DbSet<Rental>? Rentals { get; set; }
        //DbSet DbContext sınıfına özel bir list dir.
    }
}
