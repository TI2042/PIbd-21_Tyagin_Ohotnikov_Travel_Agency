using TravelAgencyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantDatabaseImplement
{
    class RestaurantDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                //optionsBuilder.UseSqlServer(@" ");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Hotel> Hotels { set; get; }
        public virtual DbSet<Guide> Guides { set; get; }
        public virtual DbSet<HotelGuide> HotelGuides { set; get; }
        public virtual DbSet<Tour> Tours { set; get; }
        public virtual DbSet<Supplier> Suppliers { set; get; }
    }
}