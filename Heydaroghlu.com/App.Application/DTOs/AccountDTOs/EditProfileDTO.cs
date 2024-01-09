using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.AccountDTOs
{
	public class EditProfileDTO
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string FatherName { get; set; }
		public string Address { get; set; }
		public string Gender { get; set; }
		public string Phone { get; set; }
		public string? CurrentPassword { get; set; }
		public string? Password { get; set; }
		public string? RepeatPassword { get; set; }
		public string? ProfileImage { get; set; }
		public string UserType { get; set; }
		public string? Longitude { get; set; }
		public string? Latitude { get; set; }
	}
}
