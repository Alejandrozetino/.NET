using Services.Interfaces;
using Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Helper;

namespace Controllers;

public class ProductsController : Controller
{
	private readonly IProductsRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IProductsRepository productRepository, IMapper mapper)
	{
        _productRepository = productRepository ??
			throw new ArgumentNullException(nameof(productRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
		;
    }

    [HttpGet]
	public async Task<ActionResult> Index()
	{
		var products = await _productRepository.ListProductsAsync();
        var productsDto = _mapper.Map<ProductosDto>(products);

        return View(productsDto);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var productDto = new ProductoDto();
		productDto.Categories = await _productRepository.ComboCategoriesAsync();

		return View("Grabar", productDto);
	}

	[HttpGet]
	public async Task<ActionResult> Editar(int id)
    {
		var product = await _productRepository.GetProductAsync(id);

		if(product == null) product = new Producto();

        var productDto = _mapper.Map<ProductoDto>(product);
        productDto.Categories = await _productRepository.ComboCategoriesAsync();
        productDto.tipoManto = Constants.Modificar;

		return View("Grabar", productDto);
    }

    [HttpGet]
	public async Task<ActionResult> Eliminar(int id)
    {
		var product = await _productRepository.GetProductAsync(id);
		var productDto = _mapper.Map<ProductoDto>(product);

        productDto.Categories = await _productRepository.ComboCategoriesAsync();
        productDto.tipoManto = Constants.Eliminar;

		return View("Grabar", productDto);
    }

	[HttpPost]
	public async Task<ActionResult> Grabar(ProductoDto model, String button)
	{
		if (button == "Salir") return RedirectToAction("Index");

        if (!ModelState.IsValid && model.tipoManto != Constants.Eliminar)
		{
            model.Categories = await _productRepository.ComboCategoriesAsync();
            return View("Grabar", model);
        }

		var product = _mapper.Map<Producto>(model);

		switch (model.tipoManto)
        {
			case Constants.Agregar:   _productRepository.AddProduct(product); break;
			case Constants.Modificar: _productRepository.UpdateProduct(product); break;
			case Constants.Eliminar:  _productRepository.DeleteProduct(product); break;
        }

		await _productRepository.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
    }
}