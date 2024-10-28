using APITask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteServicio _clienteServicio;

        public ClienteController(ClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        [HttpGet("1.obtener-info-cliente")]
        public async Task<IActionResult> ObtenerCliente()
        {
            try
            {
                var clienteDatos = await _clienteServicio.ObtenerCliente();
                return Ok(clienteDatos);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Error al consumir el servicio");
            }
        }

        [HttpGet("2.ordenar-direcciones")]
        public async Task<IActionResult> OrdenarDirecciones(string ordenarPor = "creationDate", bool ascendente = true)
        {
            try
            {
                var clienteDatos = await _clienteServicio.ObtenerCliente();
                var direcciones = clienteDatos.Addresses.AsQueryable();

                if (ordenarPor.Equals("Address1", StringComparison.OrdinalIgnoreCase))
                {
                    if (ascendente)
                    {
                        direcciones = direcciones.OrderBy(d => d.Address1);
                    }
                    else
                    {
                        direcciones = direcciones.OrderByDescending(d => d.Address1);
                    }
                }
                else if (ordenarPor.Equals("CreationDate", StringComparison.OrdinalIgnoreCase))
                {
                    if (ascendente)
                    {
                        direcciones = direcciones.OrderBy(d => d.CreationDate);
                    }
                    else
                    {
                        direcciones = direcciones.OrderByDescending(d => d.CreationDate);
                    }
                }

                return Ok(direcciones);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Error al consumir el servicio");
            }
        }

        [HttpGet("3.direccion-preferida")]
        public async Task<IActionResult> ObtenerDireccionPreferida()
        {
            try
            {
                var clienteDatos = await _clienteServicio.ObtenerCliente();
                var direccionPreferida = clienteDatos.Addresses.FirstOrDefault(d => d.Preferred);
                return Ok(direccionPreferida);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Error al consumir el servicio");
            }
        }

        [HttpGet("4.buscar-direcciones-CP")]
        public async Task<IActionResult> BuscarDireccionesPorCodigoPostal(string postalCode)
        {
            try
            {
                var clienteDatos = await _clienteServicio.ObtenerCliente();
                var direcciones = clienteDatos.Addresses.Where(d => d.PostalCode == postalCode);
                return Ok(direcciones);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Error al consumir el servicio");
            }
        }
    }
}
