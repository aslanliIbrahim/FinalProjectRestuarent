using FinalProjectRestorant.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Sms> Sms { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slides> Slides { get; set; }
        public DbSet<OurChef> ourChefs { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SteakBigMenu> SteakBigMenus { get; set; }
        public DbSet<BreakFast> breakFasts { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
    }

   




}
