using AutoMapper;
using BusinessLogic.Models;
using EntityFramework.Entities;

namespace BusinessLogic.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
		#region -Categorie-
		CreateMap<Categorie, CategorieDto>();
        CreateMap<CategorieDto, Categorie>();
		CreateMap<Categorie, AddCategorieDto>();
        CreateMap<AddCategorieDto, Categorie>();

		CreateMap<List<Categorie>, CategorieDto>(MemberList.Destination);
		#endregion
		
		#region -Car-
		CreateMap<Car, CarDto>();
        CreateMap<CarDto, Car>();

		CreateMap<List<Car>, CarDto>(MemberList.Destination);
		#endregion

		#region -User-
		CreateMap<User, UserDto>();
		CreateMap<UserDto, User>();

		CreateMap<User, CreateUserDto>();
		CreateMap<CreateUserDto, User>();

		CreateMap<List<User>, UserDto>(MemberList.Destination);
		#endregion
	}
}
