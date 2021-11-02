using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.Models;

namespace SocialNetwork.App_Start
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{
			CreateMap<ApplicationUser, UserDto>();
			CreateMap<Concert, ConcertDto>();
			CreateMap<Notification, NotificationDto>();
		}
	}
}