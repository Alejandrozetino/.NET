using Services.Interfaces;
using Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Helper;

namespace Controllers;

public class InventoriesController : Controller
{
	private readonly IInventoryRepository _inventoryRepository;
    private readonly IMapper _mapper;

    public InventoriesController(IInventoryRepository inventoryRepository, IMapper mapper)
	{
        _inventoryRepository = inventoryRepository ??
			throw new ArgumentNullException(nameof(inventoryRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
		;
    }

    [HttpGet]
	public async Task<ActionResult> Index()
	{
		var inventory = await _inventoryRepository.ListInventoryAsync();
        var inventoryDto = _mapper.Map<InventariosDto>(inventory);

        return View(inventoryDto);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var inventoryDto = new InventarioDto();
        inventoryDto.Products = await _inventoryRepository.ComboProductsAsync();
		inventoryDto.Estado = true;

		return View("Grabar", inventoryDto);
	}

	[HttpGet]
	public async Task<ActionResult> Editar(int id)
    {
		var inventory = await _inventoryRepository.GetInventoryAsync(id);

		if(inventory == null) inventory = new Inventario();

        var inventoryDto = _mapper.Map<InventarioDto>(inventory);
        inventoryDto.Products = await _inventoryRepository.ComboProductsAsync();
        inventoryDto.tipoManto = Constants.Modificar;

		return View("Grabar", inventoryDto);
    }

    [HttpGet]
	public async Task<ActionResult> Eliminar(int id)
    {
		var inventory = await _inventoryRepository.GetInventoryAsync(id);
        var inventoryDto = _mapper.Map<InventarioDto>(inventory);

        inventoryDto.Products = await _inventoryRepository.ComboProductsAsync();
        inventoryDto.tipoManto = Constants.Eliminar;

		return View("Grabar", inventoryDto);
    }

	[HttpPost]
	public async Task<ActionResult> Grabar(InventarioDto model, String button)
	{
		if (button == "Salir") return RedirectToAction("Index");

        if (!ModelState.IsValid && model.tipoManto != Constants.Eliminar)
		{
            model.Products = await _inventoryRepository.ComboProductsAsync();
            return View("Grabar", model);
        }

		var inventory = _mapper.Map<Inventario>(model);

		switch (model.tipoManto)
        {
			case Constants.Agregar:   _inventoryRepository.AddInventory(inventory); break;
			case Constants.Modificar: _inventoryRepository.UpdateInventory(inventory); break;
			case Constants.Eliminar: _inventoryRepository.DeleteInventory(inventory); break;
        }

		await _inventoryRepository.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
    }
}