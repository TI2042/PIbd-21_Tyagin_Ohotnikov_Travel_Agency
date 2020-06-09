﻿using TravelAgencyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplement
{
    class TravelAgencyDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                //DESKTOP-JQJN4MA\SQLSERVER
                //HOME-PC
                //
                optionsBuilder.UseSqlServer(@"Data Source=HOME-PC;
                    Initial Catalog=TravelAgencyDatabase;
                    Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Guide> Guides { set; get; }
        public virtual DbSet<Tour> Tours { set; get; }
        public virtual DbSet<TourGuide> TourGuides { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Hotel> Hotels { set; get; }
        public virtual DbSet<HotelGuide> HotelGuides { set; get; }
        public virtual DbSet<Request> Requests { set; get; }
        public virtual DbSet<RequestGuide> RequestGuides { set; get; }
        public virtual DbSet<Supplier> Suppliers { set; get; }
    }
}