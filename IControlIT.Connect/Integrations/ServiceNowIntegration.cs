using IControlIT.Connect.Interfaces;
using Newtonsoft.Json;
using IControlIT.Connect.Models;
using System.Threading.Tasks;

namespace IControlIT.Connect.Integrations
{
    public class ServiceNowIntegration : IExternalIntegration
    {
        private readonly ServiceNowProcessor _processor;

        public ServiceNowIntegration(ServiceNowProcessor processor)
        {
            _processor = processor;
        }

        public async Task ProcessRequestAsync(string requestData)
        {
            try
            {
                var requestObject = JsonConvert.DeserializeObject<ServiceNowRequest>(requestData);
                await _processor.ProcessServiceNowRequestAsync(requestObject);
            }
            catch (Exception ex)
            {
                // Log the exception message and stack trace
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public string SendResponse()
        {
            // Lógica para enviar uma resposta ao ServiceNow
            var response = new
            {
                Status = "Success",
                Message = "Dados processados com sucesso."
            };
            return JsonConvert.SerializeObject(response);
        }
    }
}
