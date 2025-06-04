using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

namespace lib_presentaciones
{
    public class Comunicaciones
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private string? Protocolo = "http://",
            Host = "localhost:5161",
            Servicio = ""; // Por defecto

        private string? token = null;

        public Comunicaciones(IHttpContextAccessor contextAccessor,string servicio = "",
            string protocolo = "http://",
            string host = "localhost:5161")
        {
            _contextAccessor = contextAccessor;
            Protocolo = protocolo;
            Host = host;
            Servicio = servicio;
            token = _contextAccessor.HttpContext?.Session.GetString("_AuthToken");
        }

        private void GuardarToken(string? nuevoToken)
        {
            token = nuevoToken;
            _contextAccessor.HttpContext?.Session.SetString("_AuthToken", token!);
        }

        // Este método 'ConstruirUrl' se mantiene igual, aunque no lo uses directamente para el login.
        public Dictionary<string, object> ConstruirUrl(Dictionary<string, object> data, string Metodo)
        {
            data["Url"] = Protocolo + Host + "/" + Servicio + Metodo;
            data["UrlToken"] = Protocolo + Host + "/" + Servicio + "Token/Autenticar";
            return data;
        }

        // Este método 'Ejecutar' se mantiene igual, si lo usas para otras llamadas a la API *después* de la autenticación.
        // Asume que la autenticación ya ha ocurrido y tienes un token.
        public async Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos)
        {
            string token = _contextAccessor.HttpContext?.Session.GetString("_AuthToken") ?? "";

            var request = new HttpRequestMessage(HttpMethod.Post, datos["Url"].ToString());

            if (!string.IsNullOrEmpty(token)){
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var respuesta = new Dictionary<string, object>();
            try
            {
                // Aquí, idealmente, deberías asegurarte de que 'datos' contenga un método de API a llamar
                // y que 'token' no sea nulo.
                // Si el token es nulo, deberías considerar qué hacer (¿forzar autenticación de nuevo?).

                // Se asume que 'datos' ya contiene "Url" (para la llamada real) y "UrlToken" (ya usado).
                // Y que 'token' ya fue obtenido por el método Autenticar.
                if (token == null)
                {
                    respuesta.Add("Error", "No autenticado. El token es nulo.");
                    return respuesta;
                }

                // El método 'Ejecutar' en tu código original remueve "Url" y "UrlToken"
                // y luego no usa "Url" para la llamada PostAsync.
                // Recomiendo que 'Ejecutar' reciba el endpoint al que llamar, no el diccionario con "Url".
                // Por ejemplo: Ejecutar(Dictionary<string, object> datos, string apiEndpoint)
                // Para mantenerlo con los mínimos cambios:
                var url = datos["Url"].ToString(); // Asumo que el caller ya puso la URL completa aquí
                datos.Remove("Url");
                datos.Remove("UrlToken"); // Si existen
                datos["Bearer"] = token!; // Añade el token para la llamada

                using var httpClient = new HttpClient(); // Usando 'using'
                httpClient.Timeout = new TimeSpan(0, 4, 0);
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token); // Añade el header Bearer

                var stringData = JsonConversor.ConvertirAString(datos);
                var message = await httpClient.PostAsync(url, new StringContent(stringData, Encoding.UTF8, "application/json"));

                if (!message.IsSuccessStatusCode)
                {
                    var errorContent = await message.Content.ReadAsStringAsync(); // Leer más detalles
                    respuesta.Add("Error", $"Error de comunicación: {message.ReasonPhrase}. Detalles: {errorContent}");
                    return respuesta;
                }

                var resp = await message.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(resp))
                {
                    respuesta.Add("Error", "Respuesta vacía de la API.");
                    return respuesta;
                }

                resp = Replace(resp);
                respuesta = JsonConversor.ConvertirAObjeto(resp);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.ToString();
                return respuesta;
            }
        }

        // Método de autenticación, con pequeñas mejoras para el manejo de errores de la API
        public async Task<Dictionary<string, object>> Autenticar(Dictionary<string, object> datos)
        {
            var respuesta = new Dictionary<string, object>();
            
            try
            {
                // La URL del endpoint de autenticación ya está definida aquí
                var url = Protocolo + Host + "/" + Servicio + "Token/Autenticar";

                var credenciales = new Dictionary<string, object>
                {
                    ["UserName"] = datos["UserName"],
                    ["Password"] = datos["Password"]
                };
                var stringData = JsonConversor.ConvertirAString(credenciales);

                using var httpClient = new HttpClient(); // Usando 'using' para HttpClient
                httpClient.Timeout = new TimeSpan(0, 1, 0);
                var mensaje = await httpClient.PostAsync(url, new StringContent(stringData, Encoding.UTF8, "application/json"));

                if (!mensaje.IsSuccessStatusCode)
                {
                    // Intenta leer el contenido del error de la respuesta de la API para un mensaje más útil
                    var errorContent = await mensaje.Content.ReadAsStringAsync();
                    try
                    {
                        // Si la API devuelve un JSON con el mensaje de error
                        var apiError = JsonConversor.ConvertirAObjeto(Replace(errorContent));
                        // Busca una clave que contenga el mensaje de error de tu API
                        if (apiError.ContainsKey("mensaje") || apiError.ContainsKey("error_description"))
                        {
                            respuesta.Add("Error", apiError["mensaje"]?.ToString() ?? apiError["error_description"]?.ToString() ?? "Credenciales inválidas.");
                        }
                        else
                        {
                            respuesta.Add("Error", $"Error de autenticación: {mensaje.ReasonPhrase}. Por favor, verifica tus credenciales.");
                        }
                    }
                    catch
                    {
                        // Si no es un JSON o no se pudo deserializar
                        respuesta.Add("Error", $"Error de autenticación. Código: {mensaje.StatusCode}.");
                    }
                    return respuesta;
                }

                var resp = await mensaje.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(resp))
                {
                    respuesta.Add("Error", "Respuesta vacía del servicio de autenticación.");
                    return respuesta;
                }

                resp = Replace(resp); // Sigue limpiando la respuesta si es necesario
                respuesta = JsonConversor.ConvertirAObjeto(resp);

                if (respuesta.ContainsKey("Token")) // Asegura que la respuesta contiene el token
                {
                    token = respuesta["Token"].ToString(); // Guarda el token en la instancia de Comunicaciones
                }
                else
                {
                    respuesta.Add("Error", "La respuesta de autenticación no contiene un token válido.");
                }

                if (respuesta.ContainsKey("Token"))
                {
                    var token = respuesta["Token"].ToString();
                    _contextAccessor.HttpContext?.Session.SetString("_AuthToken", token!);
                }
                else
                {
                    respuesta["Error"] = "No se recibió un token de autenticación válido.";
                }

                    return respuesta;
            }
            catch (Exception ex)
            {
                respuesta["Error"] = $"Error interno al intentar autenticar: {ex.Message}";
                // Nota: LogConversor.Log necesita un ViewData!, podrías pasarlo null o un diccionario vacío si esta clase no tiene acceso directo
                // LogConversor.Log(ex, new Dictionary<string, object>());
                return respuesta;
            }
        }

        private string Replace(string resp)
        {
            // Este método se mantiene igual, aunque es el punto más frágil de tu diseño.
            // Si tu API puede enviar JSON limpio, considera eliminar o simplificar esto.
            return resp.Replace("\\\\r\\\\n", "")
                       .Replace("\\r\\n", "")
                       .Replace("\\", "")
                       .Replace("\\\"", "\"")
                       .Replace("\"", "'")
                       .Replace("'[", "[")
                       .Replace("]'", "]")
                       .Replace("'{'", "{'")
                       .Replace("\\\\", "\\")
                       .Replace("'}'", "'}")
                       .Replace("}'", "}")
                       .Replace("\\n", "")
                       .Replace("\\r", "")
                       .Replace("    ", "")
                       .Replace("'{", "{")
                       .Replace("\"", "")
                       .Replace("  ", "")
                       .Replace("null", "''");
        }
    }
}