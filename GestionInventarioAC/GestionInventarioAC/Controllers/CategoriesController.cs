using Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Helper;
using Services.Interfaces;

namespace Controllers;

public class CategoriesController : Controller
{
	private readonly ICategoriesRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoriesRepository categoryRepository, IMapper mapper)
	{
        _categoryRepository = categoryRepository ??
			throw new ArgumentNullException(nameof(categoryRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
		;
    }

    [HttpGet]
	public async Task<ActionResult> Index()
	{
		var categories = await _categoryRepository.ListCategoriesAsync();
        var categoriesDto = _mapper.Map<CategoriasDto>(categories);

        return View(categoriesDto);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var category = new CategoriaDto();
		return View("Grabar", category);
	}

	[HttpGet]
	public async Task<ActionResult> Editar(int id)
    {
		var categorie = await _categoryRepository.GetCategoryAsync(id);

		if(categorie == null) categorie = new Categoria();

        var categorieDto = _mapper.Map<CategoriaDto>(categorie);
        categorieDto.tipoManto = Constants.Modificar;

		return View("Grabar", categorieDto);
    }

    [HttpGet]
	public async Task<ActionResult> Eliminar(int id)
    {
		var categorie = await _categoryRepository.GetCategoryAsync(id);
		var categorieDto = _mapper.Map<CategoriaDto>(categorie);
        categorieDto.tipoManto = Constants.Eliminar;

		return View("Grabar", categorieDto);
    }

	[HttpPost]
	public async Task<ActionResult> Grabar(CategoriaDto model, String button)
	{
		if (button == "Salir") return RedirectToAction("Index");

        if (!ModelState.IsValid && model.tipoManto != Constants.Eliminar) return View("Grabar", model);

		var categorie = _mapper.Map<Categoria>(model);

		switch (model.tipoManto)
        {
			case Constants.Agregar:   _categoryRepository.AddCategory(categorie); break;
			case Constants.Modificar: _categoryRepository.UpdateCategory(categorie); break;
			case Constants.Eliminar: _categoryRepository.DeleteCategory(categorie); break;
        }

		await _categoryRepository.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
    }
}