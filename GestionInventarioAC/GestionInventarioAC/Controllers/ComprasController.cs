using Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Helper;
using Services.Interfaces;

namespace Controllers;

public class ComprasController : Controller
{
	private readonly IComprasRepository _comprasRepository;
    private readonly IMapper _mapper;

    public ComprasController(IComprasRepository comprasRepository, IMapper mapper)
	{
        _comprasRepository = comprasRepository ??
			throw new ArgumentNullException(nameof(comprasRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
		;
    }

    [HttpGet]
	public async Task<ActionResult> Index()
	{
		var categories = await _comprasRepository.ListComprasAsync();
        var categoriesDto = _mapper.Map<ComprasDto>(categories);

        return View(categoriesDto);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var category = new CompraDto();
		category.Proveedores = await _comprasRepository.ComboProveedoresAsync();

		return View("Grabar", category);
	}

	[HttpGet]
	public async Task<ActionResult> Editar(int id)
    {
		var categorie = await _comprasRepository.GetCompra(id);

		if(categorie == null) categorie = new Compra();

        var categorieDto = _mapper.Map<CompraDto>(categorie);
        categorieDto.Proveedores = await _comprasRepository.ComboProveedoresAsync();
        categorieDto.TipoManto = Constants.Modificar;

		return View("Grabar", categorieDto);
    }

    [HttpGet]
	public async Task<ActionResult> Eliminar(int id)
    {
		var categorie = await _comprasRepository.GetCompra(id);
		var categorieDto = _mapper.Map<CompraDto>(categorie);
        categorieDto.Proveedores = await _comprasRepository.ComboProveedoresAsync();
        categorieDto.TipoManto = Constants.Eliminar;

		return View("Grabar", categorieDto);
    }

	[HttpPost]
	public async Task<ActionResult> Grabar(CompraDto model, String button)
	{
		if (button == "Salir") return RedirectToAction("Index");

        if (!ModelState.IsValid && model.TipoManto != Constants.Eliminar)
		{
            model.Proveedores = await _comprasRepository.ComboProveedoresAsync();
            return View("Grabar", model);
        }
		
		var categorie = _mapper.Map<Compra>(model);

		switch (model.TipoManto)
        {
			case Constants.Agregar: _comprasRepository.AddCompra(categorie); break;
			case Constants.Modificar: _comprasRepository.UpdateCompra(categorie); break;
			case Constants.Eliminar: _comprasRepository.RemoveCompra(categorie); break;
        }

		await _comprasRepository.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
    }
}