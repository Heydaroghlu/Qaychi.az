using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.AccountDTOs
{
	public class MyAccountDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string? FatherName { get; set; }
		public string? Address { get; set; }
		public string Gender { get; set; }
		public string ProfileImg { get; set; }
		public string UserType { get; set; }
		public string? Voen { get; set; }
		public int OTP { get; set; }
		public string? Longitude { get; set; }
		public string? Latitude { get; set; }
	}
}
