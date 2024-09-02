using Microsoft.AspNetCore.Mvc;
using IControlIT.Connect.Interfaces;
using IControlIT.Connect.Models;
using System.Threading.Tasks;

namespace IControlIT.Connect.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceNowController : ControllerBase
    {
        private readonly IExternalIntegration _serviceNowIntegration;
        private readonly ServiceNowProcessor _processor;

        public ServiceNowController(IExternalIntegration serviceNowIntegration, ServiceNowProcessor processor)
        {
            _serviceNowIntegration = serviceNowIntegration;
            _processor = processor;
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
        public async Task<IActionResult> ProcessData([FromBody] ServiceNowRequest requestData)
        {
            if (requestData == null)
            {
                return BadRequest("Invalid data.");
            }

            await _processor.ProcessServiceNowRequestAsync(requestData);

            return Ok("Request processed successfully");
        }
    }
}
