using Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Helper;
using Services.Interfaces;
using Services;

namespace Controllers;

public class VentasController : Controller
{
	private readonly IVentasRepository _ventasRepository;
    private readonly IMapper _mapper;

    public VentasController(IVentasRepository categoryRepository, IMapper mapper)
	{
        _ventasRepository = categoryRepository ??
			throw new ArgumentNullException(nameof(categoryRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
		;
    }

    [HttpGet]
	public async Task<ActionResult> Index()
	{
		var categories = await _ventasRepository.ListVentasAsync();
        var categoriesDto = _mapper.Map<VentasDto>(categories);

        return View(categoriesDto);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var category = new VentaDto();
        category.Clientes = await _ventasRepository.ComboClientesAsync();

        return View("Grabar", category);
	}

	[HttpGet]
	public async Task<ActionResult> Editar(int id)
    {
		var categorie = await _ventasRepository.GetVenta(id);

		if(categorie == null) categorie = new Venta();

        var categorieDto = _mapper.Map<VentaDto>(categorie);
        categorieDto.Clientes = await _ventasRepository.ComboClientesAsync();
        categorieDto.TipoManto = Constants.Modificar;

		return View("Grabar", categorieDto);
    }

    [HttpGet]
	public async Task<ActionResult> Eliminar(int id)
    {
		var categorie = await _ventasRepository.GetVenta(id);
		var categorieDto = _mapper.Map<VentaDto>(categorie);

        categorieDto.Clientes = await _ventasRepository.ComboClientesAsync();
        categorieDto.TipoManto = Constants.Eliminar;

		return View("Grabar", categorieDto);
    }

	[HttpPost]
	public async Task<ActionResult> Grabar(VentaDto model, String button)
	{
		if (button == "Salir") return RedirectToAction("Index");

        if (!ModelState.IsValid && model.TipoManto != Constants.Eliminar)
		{
            model.Clientes = await _ventasRepository.ComboClientesAsync();
            return View("Grabar", model);
        }
		
		var categorie = _mapper.Map<Venta>(model);

		switch (model.TipoManto)
		{
			case Constants.Agregar: _ventasRepository.AddVenta(categorie); break;
			case Constants.Modificar: _ventasRepository.UpdateVenta(categorie); break;
			case Constants.Eliminar: _ventasRepository.RemoveVenta(categorie); break;
		}

		await _ventasRepository.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
    }
}