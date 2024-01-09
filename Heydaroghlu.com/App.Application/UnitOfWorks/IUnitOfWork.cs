using App.Application.Repositories;
using App.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IRepository<AppUser> RepositoryAppUser { get; set; }
        public IRepository<Category> RepositoryCategory { get; set; }
        public IRepository<Reclam> RepositoryReclam { get; set; }
        public IRepository<Store> RepositoryStore { get; set; }
        public IRepository<StoreImage> RepositoryStoreImage { get; set; }
        public IRepository<Service > RepositoryService { get; set; }
        public IRepository<Wishlist> RepositoryWishlist { get; set; }   
		Task<int> CommitAsync();
        int Commit();
    }
}
