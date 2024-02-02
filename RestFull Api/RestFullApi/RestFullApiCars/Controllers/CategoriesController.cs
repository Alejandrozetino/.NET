using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Models;
using EntityFramework.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestFullApiCars.Controllers;

[ApiController]
[Route("api/Categories")]
public class CategoriesController : ControllerBase
{
	private readonly ICategorieRepository _categoryRepository;
	private readonly IMapper _mapper;

	public CategoriesController(ICategorieRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	[HttpGet]
	[ResponseCache(CacheProfileName = "PorDefecto20seg")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAllCategories()
	{
		var categories = await _categoryRepository.GetAllCategoriesAsync();

		return Ok(categories.ToModel());
	}

	[ResponseCache(CacheProfileName = "PorDefecto20seg")]
	[HttpGet("{categoriaId:int}", Name = "GetCategorie")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetCategorie(int categoriaId)
	{
		var categorie = await _categoryRepository.GetCategoryAsync(categoriaId);

		if(categorie == null)
		{
			return NotFound();
		}

		return Ok(categorie.ToModel());
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(201, Type = typeof(AddCategorieDto))]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateCategorie([FromBody] AddCategorieDto categorieDto)
	{
		if (!ModelState.IsValid || categorieDto == null)
		{
			return BadRequest(ModelState);
		}

		if(await _categoryRepository.CategoryExistsAsync(null, categorieDto.Name))
		{
			ModelState.AddModelError("Error", "La categoria ya existe");
			return StatusCode(404, ModelState);
		}

		var grabo = await _categoryRepository.AddCategoryAsync(categorieDto.ToEntity());

		if (grabo == 0)
		{
			ModelState.AddModelError("Error", $"Algo salió mal al crear el registro {categorieDto.Name}");
			return StatusCode(500, ModelState);
		}

		return Ok("Registro creado correctamente");
	}

	[Authorize(Roles = "Admin")]
	[HttpPatch("{categoriaId:int}", Name = "UpdateCategorie")]
	[ProducesResponseType(201, Type = typeof(CategorieDto))]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateCategorie(int categoriaId, [FromBody] CategorieDto categorieDto)
	{
		if (!ModelState.IsValid || (categorieDto == null || categoriaId != categorieDto.Id) )
		{
			return BadRequest(ModelState);
		}

		var grabo = await _categoryRepository.UpdateCategoryAsync(categorieDto.ToEntity());

		if (grabo == 0)
		{
			ModelState.AddModelError("Error", $"Algo salió mal al actualizar el registro {categorieDto.Name}");
			return StatusCode(500, ModelState);
		}

		return Ok("Registro actualizado correctamente");
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("{categoriaId:int}", Name = "RemoveCategorie")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> RemoveCategorie(int categoriaId)
	{
		if (!await _categoryRepository.CategoryExistsAsync(categoriaId, null))
		{
			return NotFound("La categoria no existe.");
		}

		var categorie = await _categoryRepository.GetCategoryAsync(categoriaId);
		var grabo = await _categoryRepository.DeleteCategoryAsync(categorie);

		if (grabo == 0)
		{
			ModelState.AddModelError("Error", $"Algo salió mal al elimiinar el registro {categoriaId}");
			return StatusCode(500, ModelState);
		}

		return Ok("Registro eliminado correctamente");
	}
}
