using Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Helper;
using Services.Interfaces;

namespace Controllers;

public class ClientesController : Controller
{
    private readonly IClientesRepository _clientesRepository;
    private readonly IMapper _mapper;

    public ClientesController(IClientesRepository categoryRepository, IMapper mapper)
    {
        _clientesRepository = categoryRepository ??
            throw new ArgumentNullException(nameof(categoryRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
        ;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var categories = await _clientesRepository.ListClientesAsync();
        var categoriesDto = _mapper.Map<ClientesDto>(categories);

        return View(categoriesDto);
    }

    [HttpGet]
    public async Task<ActionResult> Agregar()
    {
        var category = new ClienteDto();
        return View("Grabar", category);
    }

    [HttpGet]
    public async Task<ActionResult> Editar(int id)
    {
        var categorie = await _clientesRepository.GetCliente(id);

        if (categorie == null) categorie = new Cliente();

        var categorieDto = _mapper.Map<ClienteDto>(categorie);
        categorieDto.TipoManto = Constants.Modificar;

        return View("Grabar", categorieDto);
    }

    [HttpGet]
    public async Task<ActionResult> Eliminar(int id)
    {
        var categorie = await _clientesRepository.GetCliente(id);
        var categorieDto = _mapper.Map<ClienteDto>(categorie);
        categorieDto.TipoManto = Constants.Eliminar;

        return View("Grabar", categorieDto);
    }

    [HttpPost]
    public async Task<ActionResult> Grabar(ClienteDto model, String button)
    {
        if (button == "Salir") return RedirectToAction("Index");

        if (!ModelState.IsValid && model.TipoManto != Constants.Eliminar) return View("Grabar", model);

        var categorie = _mapper.Map<Cliente>(model);

        switch (model.TipoManto)
        {
            case Constants.Agregar: _clientesRepository.AddCliente(categorie); break;
            case Constants.Modificar: _clientesRepository.UpdateCliente(categorie); break;
            case Constants.Eliminar: _clientesRepository.RemoveCliente(categorie); break;
        }

        await _clientesRepository.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}