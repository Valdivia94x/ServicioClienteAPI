using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using APITask.Models;
using System.Text.RegularExpressions;

namespace APITask.Services
{
    public class ClienteServicio
    {
        private readonly HttpClient _httpClient;

        public ClienteServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://examentecnico.azurewebsites.net/v3/api/Test/Customer");
            _httpClient.DefaultRequestHeaders.Add("Device", "POSTMAN");
            _httpClient.DefaultRequestHeaders.Add("Version", "2.0.6.0");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y2hyaXN0b3BoZXJAZGV2ZWxvcC5teDpUZXN0aW5nRGV2ZWxvcDEyM0AuLi4=");
        }

        public async Task<Cliente> ObtenerCliente()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                //Prueba para entender el formato json
                /*string respuesta = """{"customerId": "cdjGV9JluCUeMYFkn0wdsc27trCa", "authType": "registered"}""";
                Console.WriteLine(respuesta);*/

                var respuesta = await response.Content.ReadAsStringAsync();
                respuesta = FormatearRespuesta(respuesta);
                Console.WriteLine(respuesta);

                var cliente = JsonSerializer.Deserialize<Cliente>(respuesta);

                if (cliente == null)
                {
                    throw new Exception("No se pudo obtener el cliente");
                }

                return cliente;
            }
            else
            {
                throw new Exception($"Error:{response.StatusCode}");
            }
        }

        private string FormatearRespuesta(string respuesta)
        {
            respuesta = respuesta.Replace("\\r\\n", "").Replace("\\", "").Trim();
            respuesta = Regex.Replace(respuesta, @"\s+(?=(\""|:))", "");
            respuesta = Regex.Replace(respuesta, @"null\s+}", "null}");
            respuesta = Regex.Replace(respuesta, @",\s+{", ",{");
            respuesta = Regex.Replace(respuesta, @"\[\s+{", "[{");
            respuesta = Regex.Replace(respuesta, @"}\s+]", "}]");
            if (respuesta.StartsWith("\"") && respuesta.EndsWith("\""))
            {
                respuesta = respuesta.Substring(1, respuesta.Length - 2);
            }
            return respuesta;
        }
    }
}
