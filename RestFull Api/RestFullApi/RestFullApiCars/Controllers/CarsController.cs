using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Models;
using EntityFramework.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestFullApiCars.Controllers;

[ApiController]
[Route("api/Cars")]
public class CarsController : ControllerBase
{
	private readonly ICarRepository _carRepository;
	private readonly ICategorieRepository _categorieRepository;
	private readonly IMapper _mapper;

	public CarsController(ICarRepository carRepository, ICategorieRepository categorieRepository, IMapper mapper)
	{
		_carRepository = carRepository;
		_categorieRepository = categorieRepository;
		_mapper = mapper;
	}

	[HttpGet]
	//[ResponseCache(Duration = 20)]
	[ResponseCache(CacheProfileName = "PorDefecto20seg")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAllCars()
	{
		var cars = await _carRepository.GetAllCarsAsync();

		return Ok(cars.ToModel());
	}

	[HttpGet("{carId:int}", Name = "GetCar")]
	[ResponseCache(CacheProfileName = "PorDefecto20seg")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetCar(int carId)
	{
		var car = await _carRepository.GetCarAsync(carId);

		if(car == null)
		{
			return NotFound();
		}

		return Ok(car.ToModel());
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(201, Type = typeof(CarDto))]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateCar([FromBody] CarDto carDto)
	{
		if (!ModelState.IsValid || carDto == null)
		{
			return BadRequest(ModelState);
		}

		if(await _carRepository.CarExistsAsync(null, carDto.Name))
		{
			ModelState.AddModelError("Error", "El automovil ya existe");
			return StatusCode(404, ModelState);
		}

		if (!await _categorieRepository.CategoryExistsAsync(carDto.IdCategoria, null))
		{
			ModelState.AddModelError("Error", "La categoria seleccionada no existe");
			return StatusCode(404, ModelState);
		}

		var grabo = await _carRepository.AddCarAsync(carDto.ToEntity());

		if (grabo == 0)
		{
			ModelState.AddModelError("Error", $"Algo salió mal al crear el registro {carDto.Name}");
			return StatusCode(500, ModelState);
		}

		return Ok("Registro creado correctamente");
	}

	[Authorize(Roles = "Admin")]
	[HttpPatch("{carId:int}", Name = "UpdateCar")]
	[ProducesResponseType(201, Type = typeof(CarDto))]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateCar(int carId, [FromBody] CarDto carDto)
	{
		if (!ModelState.IsValid || (carDto == null || carId != carDto.Id) )
		{
			return BadRequest(ModelState);
		}

		if (!await _categorieRepository.CategoryExistsAsync(carDto.IdCategoria, null))
		{
			ModelState.AddModelError("Error", "La categoria seleccionada no existe");
			return StatusCode(404, ModelState);
		}

		var grabo = await _carRepository.UpdateCarAsync(carDto.ToEntity());

		if (grabo == 0)
		{
			ModelState.AddModelError("Error", $"Algo salió mal al actualizar el registro {carDto.Name}");
			return StatusCode(500, ModelState);
		}

		return Ok("Registro actualizado correctamente");
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("{carId:int}", Name = "RemoveCar")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> RemoveCar(int carId)
	{
		if (!await _carRepository.CarExistsAsync(carId, null))
		{
			return NotFound("El automovil no existe.");
		}

		var car = await _carRepository.GetCarAsync(carId);
		var grabo = await _carRepository.DeleteCarAsync(car);

		if (grabo == 0)
		{
			ModelState.AddModelError("Error", $"Algo salió mal al elimiinar el registro {carId}");
			return StatusCode(500, ModelState);
		}

		return Ok("Registro eliminado correctamente");
	}

	[HttpGet("SearchCarForCategorie/{categorieId:int}")]
	public async Task<IActionResult> SearchCarForCategorie(int categorieId)
	{
		var cars = await _carRepository.SearchCarsForCategorieAsync(categorieId);

		if (cars == null)
		{
			return NotFound();
		}

		return Ok(cars.ToModel());
	}

	[HttpGet("SearchCarForName")]
	public async Task<IActionResult> SearchCarForName(string nameCar)
	{
		var cars = await _carRepository.SearchCarsForNameAsync(nameCar);

		if (cars == null)
		{
			return NotFound();
		}

		return Ok(cars.ToModel());
	}
}
