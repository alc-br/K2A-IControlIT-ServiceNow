using Microsoft.AspNetCore.Mvc;
using IControlIT.Connect.Interfaces;
using IControlIT.Connect.Models;
using Newtonsoft.Json;

namespace IControlIT.Connect.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceNowController : ControllerBase
    {
        private readonly IExternalIntegration _serviceNowIntegration;

        public ServiceNowController(IExternalIntegration serviceNowIntegration)
        {
            _serviceNowIntegration = serviceNowIntegration;
        }

        /// <summary>
        /// Processa os dados recebidos do ServiceNow.
        /// </summary>
        /// <param name="requestData">Dados do ServiceNow encapsulados na classe ServiceNowRequest.</param>
        /// <returns>Retorna um status 200 OK se bem sucedido, ou 400 Bad Request se os dados forem inválidos.</returns>
        [HttpPost("process")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult ProcessData([FromBody] ServiceNowRequest requestData)
        {
            if (requestData == null)
            {
                return BadRequest("Invalid data.");
            }

            _serviceNowIntegration.ProcessRequest(JsonConvert.SerializeObject(requestData));
            return Ok("Request processed successfully");
        }
    }
}
