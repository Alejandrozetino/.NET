using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Profiles;
using EntityFramework.Entities;

namespace BusinessLogic.Mapper;

public static class Mappers
{
    internal static IMapper Mapper { get; }

    static Mappers()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>())
            .CreateMapper();
    }

	#region -Categoria-
	public static CategorieDto ToModel(this Categorie categorie)
    {
        return Mapper.Map<CategorieDto>(categorie);
    }

    public static Categorie ToEntity(this CategorieDto categorieDto)
    {
        return Mapper.Map<Categorie>(categorieDto);
    }

    public static Categorie ToEntity(this AddCategorieDto categorieDto)
    {
        return Mapper.Map<Categorie>(categorieDto);
    }

	public static List<CategorieDto> ToModel(this List<Categorie> categories)
	{
		return Mapper.Map<List<CategorieDto>>(categories);
	}
	#endregion

	#region -Car-
	public static CarDto ToModel(this Car car)
	{
		return Mapper.Map<CarDto>(car);
	}

	public static Car ToEntity(this CarDto carDto)
	{
		return Mapper.Map<Car>(carDto);
	}

	public static List<CarDto> ToModel(this List<Car> cars)
	{
		return Mapper.Map<List<CarDto>>(cars);
	}
	#endregion

	#region -User-
	public static UserDto ToModel(this User user)
	{
		return Mapper.Map<UserDto>(user);
	}

	public static User ToEntity(this UserDto userDto)
	{
		return Mapper.Map<User>(userDto);
	}

	public static User ToEntity(this CreateUserDto userDto)
	{
		return Mapper.Map<User>(userDto);
	}

	public static List<UserDto> ToModel(this List<User> users)
	{
		return Mapper.Map<List<UserDto>>(users);
	}
	#endregion
}
