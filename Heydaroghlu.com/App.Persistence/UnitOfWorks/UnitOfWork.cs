using App.Application.Repositories;
using App.Application.UnitOfWorks;
using App.Domain.Entitites;
using App.Persistence.Contexts;
using App.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context= context;
            RepositoryAppUser = new Repository<AppUser>(_context);
			RepositoryCategory=new Repository<Category>(_context);
			RepositoryReclam=new Repository<Reclam>(_context);
			RepositoryStore=new Repository<Store>(_context);
			RepositoryStoreImage=new Repository<StoreImage>(_context);
			RepositoryService=new Repository<Service>(_context);
			RepositoryWishlist=new Repository<Wishlist>(_context);	

		}
        public IRepository<AppUser> RepositoryAppUser { get; set; }
		public IRepository<Category> RepositoryCategory { get; set; }
		public IRepository<Reclam> RepositoryReclam { get; set; }
		public IRepository<Store> RepositoryStore { get; set; }
		public IRepository<StoreImage> RepositoryStoreImage { get; set; }

		public IRepository<Service> RepositoryService { get; set; }
		public IRepository<Wishlist> RepositoryWishlist { get; set; }


		public int Commit()
		{
			return _context.SaveChanges();
		}

		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
