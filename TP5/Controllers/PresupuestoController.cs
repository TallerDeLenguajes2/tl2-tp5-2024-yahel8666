using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class PrespuestoController : ControllerBase
{
    PresupuestoRepository _presupuestoRepository;

    public PrespuestoController()
    {
        _presupuestoRepository = new PresupuestoRepository();
    }

    [HttpPost("CrearPresupuesto")]
    public ActionResult<Presupuesto> CrearPresupuesto([FromBody] Presupuesto nuevoP)
    {
        _presupuestoRepository.Create(nuevoP);
        return Ok(nuevoP);
    }

    [HttpPost("ProductoDetalle/{id}")]
    public ActionResult<Presupuesto> AgregarProductoAlPresupuesto(int id)
    {
        if(!_presupuestoRepository.AgregarProducto(id)) return NotFound("No se encuentró producto con ese id");
        else return Ok("Producto agregado");
    }

    [HttpGet("ListarPresupuestos")]
    public ActionResult<List<Presupuesto>> ListarPresupuestos()
    {
        var presupuestos = _presupuestoRepository.ListarPresupuestos();
        if (presupuestos != null) return NotFound("No hay presupuestos para mostrar");
        else return Ok(presupuestos);
    }

    // [HttpGet("GetPresupuesto/{id}")]
    // public ActionResult <Presupuesto> GetPresupuestoById(int id)
    // {
    //     var presupuesto = _presupuestoRepository.GetPresupuestoById(id); 
    //     if (presupuesto != null) return NotFound("No se encontro el presupuesto"); 
    //     else return Ok(presupuesto); 
    // }

}