using Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Helper;
using Services.Interfaces;

namespace Controllers;

public class ProveedoresController : Controller
{
	private readonly IProveedoresRepository _proveedoresRepository;
    private readonly IMapper _mapper;

    public ProveedoresController(IProveedoresRepository categoryRepository, IMapper mapper)
	{
        _proveedoresRepository = categoryRepository ??
			throw new ArgumentNullException(nameof(categoryRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
		;
    }

    [HttpGet]
	public async Task<ActionResult> Index()
	{
		var categories = await _proveedoresRepository.ListProveedoresAsync();
        var categoriesDto = _mapper.Map<ProveedoresDto>(categories);

        return View(categoriesDto);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var category = new ProveedorDto();
		return View("Grabar", category);
	}

	[HttpGet]
	public async Task<ActionResult> Editar(int id)
    {
		var categorie = await _proveedoresRepository.GetProveedor(id);

		if(categorie == null) categorie = new Proveedor();

        var categorieDto = _mapper.Map<ProveedorDto>(categorie);
        categorieDto.TipoManto = Constants.Modificar;

		return View("Grabar", categorieDto);
    }

    [HttpGet]
	public async Task<ActionResult> Eliminar(int id)
    {
		var categorie = await _proveedoresRepository.GetProveedor(id);
		var categorieDto = _mapper.Map<ProveedorDto>(categorie);
        categorieDto.TipoManto = Constants.Eliminar;

		return View("Grabar", categorieDto);
    }

	[HttpPost]
	public async Task<ActionResult> Grabar(ProveedorDto model, String button)
	{
		if (button == "Salir") return RedirectToAction("Index");

        if (!ModelState.IsValid && model.TipoManto != Constants.Eliminar) return View("Grabar", model);

		var categorie = _mapper.Map<Proveedor>(model);

		switch (model.TipoManto)
		{
			case Constants.Agregar: _proveedoresRepository.AddProveedor(categorie); break;
			case Constants.Modificar: _proveedoresRepository.UpdateProveedor(categorie); break;
			case Constants.Eliminar: _proveedoresRepository.RemoveProveedor(categorie); break;
		}

		await _proveedoresRepository.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
    }
}