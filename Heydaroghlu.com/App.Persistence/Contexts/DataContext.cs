using App.Domain.Base;
using App.Domain.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.Contexts
{
    public class DataContext:IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reclam> Reclams { get; set; }
	    public DbSet<Store> Stores { get; set; }    
        public DbSet<StoreImage> StoreImages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
        {
            var entites = ChangeTracker.Entries<BaseEntity>();
            foreach (var entite in entites)
            {
                if(entite.State==EntityState.Added)
                {
					entite.Entity.CreatedDate = DateTime.UtcNow.AddHours(4);

				}
                else if(entite.State==EntityState.Modified)
                {
                    entite.Entity.LastModifiedDate= DateTime.UtcNow.AddHours(4);

				}
			}
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
