using App.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Wishlist
{
	public class WishlistPostDTO
	{
		public string UserId { get; set; }
		public int StoreId { get; set; }
	}
}
