using App.Application.DTOs.AccountDTOs;
using App.Application.DTOs.CategoryDTOs;
using App.Application.DTOs.ImageDTOs;
using App.Application.DTOs.ReclamDTOs;
using App.Application.DTOs.ServiceDTOs;
using App.Application.DTOs.StoreDTOs;
using App.Application.DTOs.Wishlist;
using App.Domain.Entitites;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Profiles
{
	public class MapperProfile:Profile
	{
        public MapperProfile()
        {
			CreateMap<AppUser, MyAccountDTO>();

			CreateMap<CategoryCReateDTO, Category>();
			CreateMap<Category, CategoryReturnDTO>();

			CreateMap<ReclamCreateDTO, Reclam>();
			CreateMap<Reclam, ReclamReturnDTO>();

			CreateMap<StoreCreateDTO,Store>();
			CreateMap<ImagePostDTO, StoreImage>();
			CreateMap<StoreImage, ImagePostDTO>();

			CreateMap<Store, StoreReturnDTO>();

			CreateMap<Service, ServicePostDTO>();
			CreateMap<ServicePostDTO, Service>();

			CreateMap<WishlistPostDTO, Wishlist>();
			CreateMap<Wishlist, WishlistPostDTO>();

		}
	}
}
