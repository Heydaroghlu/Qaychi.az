using App.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entitites
{
	public class Reclam:BaseEntity
	{
		public string Title { get; set; }
		public string Title2 { get; set; }
		public decimal DiscountPercent { get; set; }
		public string Description { get; set; }
		public string? BackgrooundImage { get; set; }
	}
}
