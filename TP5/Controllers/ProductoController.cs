namespace TP5;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase  
{
    ProductoRepository _productoRepository;

    public ProductoController()
    {
        _productoRepository = new ProductoRepository();
    }

    [HttpPost("Create")]
    public ActionResult Create([FromBody] Producto p)
    {
        if (_productoRepository.CrearProducto(p))
            return Ok("Producto creado");
        else
            return BadRequest("Error al crear el producto");
    }

    [HttpGet("GetAll")]
    public ActionResult<List<Producto>> GetAll(){
        var productos = _productoRepository.GetAll();
        if (productos is null) return BadRequest(); 
        else return Ok(productos); 
    }

    // [HttpDelete("Delete/{id}")]
    // public ActionResult Delete(int id)
    // {
    //     if(_productoRepository.Delete(id)) return Ok(); 
    //     else return NotFound("No se encontro el id"); 
    // }

    [HttpPut("Update/{id}")]
    public ActionResult Update(int id, [FromBody] Producto producto)
    {
        if (producto == null || string.IsNullOrWhiteSpace(producto.Descripcion))
        {
            return BadRequest("El nombre del producto no puede estar vacío.");
        }
        var exito = _productoRepository.ActualizarProducto(id, producto.Descripcion);
        if (exito)
        {
            return Ok("Producto actualizado correctamente.");
        }
        else
        {
            return NotFound("Producto no encontrado.");
        }
    }
}

