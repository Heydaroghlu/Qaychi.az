using App.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entitites
{
	public class Wishlist : BaseEntity
	{
		public string UserId { get; set; }
		public AppUser User { get; set; }
		public int StoreId { get; set; }
		public Store Store { get; set; }	
	}
}
