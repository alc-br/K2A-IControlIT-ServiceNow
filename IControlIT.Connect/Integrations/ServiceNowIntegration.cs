using IControlIT.Connect.Interfaces;
using Newtonsoft.Json;
using IControlIT.Connect.Models;

namespace IControlIT.Connect.Integrations
{
    public class ServiceNowIntegration : IExternalIntegration
    {
        public void ProcessRequest(string requestData)
        {
            try
            {
                var requestObject = JsonConvert.DeserializeObject<ServiceNowRequest>(requestData);

                // Simplesmente retorne o objeto deserializado como um teste
                Console.WriteLine(JsonConvert.SerializeObject(requestObject));

                string consumerName = requestObject.UserName;
                string consumerId = GetConsumerIdByName(consumerName);

                // Inserir ou atualizar dados no IControlIT conforme necessário
                //InsertOrUpdateSolicitation(consumerId, requestObject);
            }
            catch (Exception ex)
            {
                // Log the exception message and stack trace
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        private string GetConsumerIdByName(string name)
        {
            // Lógica para buscar o Id_Consumidor no banco de dados
            return "exemplo-id";
        }

        private void InsertOrUpdateSolicitation(string consumerId, ServiceNowRequest request)
        {
            // Lógica para inserir ou atualizar a solicitação na tabela dbo.Solicitacao
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
