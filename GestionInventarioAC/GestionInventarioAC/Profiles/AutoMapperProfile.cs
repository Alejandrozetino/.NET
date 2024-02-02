using Helper;
using Models;
using AutoMapper;
using Entities;

namespace Profiles;
public class AutoMapperProfile: Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Categoria, CategoriaDto>();
		CreateMap<CategoriaDto, Categoria>();
        CreateMap<Producto, ProductoDto>();
        CreateMap<ProductoDto, Producto>();
        CreateMap<Inventario, InventarioDto>();
        CreateMap<InventarioDto, Inventario>();
        CreateMap<Proveedor, ProveedorDto>();
        CreateMap<ProveedorDto, Proveedor>();
        CreateMap<Cliente, ClienteDto>();
        CreateMap<ClienteDto, Cliente>();
        CreateMap<Compra, CompraDto>();
        CreateMap<CompraDto, Compra>();
        CreateMap<Venta, VentaDto>();
        CreateMap<VentaDto, Venta>();

        CreateMap<RecordList<Categoria>, CategoriasDto>(MemberList.Destination)
            .ForMember(x => x.Categories, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<Producto>, ProductosDto>(MemberList.Destination)
            .ForMember(x => x.Products, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<Inventario>, InventariosDto>(MemberList.Destination)
            .ForMember(x => x.Inventory, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<Proveedor>, ProveedoresDto>(MemberList.Destination)
            .ForMember(x => x.Proveedores, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<Cliente>, ClientesDto>(MemberList.Destination)
            .ForMember(x => x.Clientes, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<Compra>, ComprasDto>(MemberList.Destination)
            .ForMember(x => x.Compras, opt => opt.MapFrom(src => src.Data));

        CreateMap<RecordList<Venta>, VentasDto>(MemberList.Destination)
            .ForMember(x => x.Ventas, opt => opt.MapFrom(src => src.Data));
    }
}
