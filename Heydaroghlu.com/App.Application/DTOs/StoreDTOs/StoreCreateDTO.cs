using App.Application.DTOs.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.StoreDTOs
{
	public class StoreCreateDTO
	{
		public string OwnerId { get; set; }
		public string Name { get; set; } //Nazli 
		public string Title { get; set; }   // gozellik salonu
		public string Description { get; set; } //20 ildir poxun icindeyik
		public string PosterUrl { get; set; } //www.img.com
		public string Address { get; set; } //Werifzade kucesi 12
		public string Voen { get; set; }
		public string Phone { get; set; } // 7100910
		public string Instagram { get; set; }
		public string WebSite { get; set; }
		public string atWeek { get; set; }
		public string Weekend { get; set; }
		public int CategoryId { get; set; }
		public string? Longitude { get; set; }
		public string? Latitude { get; set; }
		public List<ImagePostDTO> Images { get; set; }
	}
}
