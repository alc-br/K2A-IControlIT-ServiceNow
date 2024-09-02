//IControlIT.Connect\Models\ServiceNowRequest.cs
namespace IControlIT.Connect.Models
{
    public class ServiceNowRequest
    {
        public string Name { get; set; }                   // Novo campo
        public string Action { get; set; }                 // Novo campo
        public string CorrelationId { get; set; }          // requestNumber
        public string Number { get; set; }                 // workOrderNumber
        public string Comments { get; set; }               // additionalInformation
        public string AssignedTo { get; set; }             // userName
        public string UserName { get; set; }               // userName (redundância possível)
        public string UserNumber { get; set; }             // userNumber
        public string DesignationProduct { get; set; }     // designationProduct
        public string TelecomProvider { get; set; }        // telecomProvider
        public string FramingPlan { get; set; }            // framingPlan
        public string MigrationDevice { get; set; }        // migrationDevice
        public string ServicePack { get; set; }            // servicePack
        public string NewAreaCode { get; set; }            // newAreaCode
        public string NewUserNumber { get; set; }          // newUserNumber
        public string NewTelecomProvider { get; set; }     // newTelecomProvider
        public string CountryDateForRoaming { get; set; }  // countryDateForRoaming
        public string TransactionID { get; set; }          // transactionID
        public string ManagerOrAdm { get; set; }           // Novo campo
        public string ViewProfile { get; set; }            // Novo campo
        public string ManagerNumber { get; set; }          // Novo campo

        // Adicionando os campos necessários
        public int ChamadoId { get; set; }
        public int ConsumidorId { get; set; }
        public int AtivoId { get; set; }
        public int ConglomeradoId { get; set; }
        public int PlanoId { get; set; }
    }
}
