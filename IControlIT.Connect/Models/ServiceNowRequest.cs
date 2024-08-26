namespace IControlIT.Connect.Models
{
    public class ServiceNowRequest
    {
        public string CorrelationId { get; set; }         // requestNumber
        public string Number { get; set; }                // workOrderNumber
        public string State { get; set; }
        public string Comments { get; set; }              // additionalInformation
        public string AssignedTo { get; set; }            // userName
        public string UserName { get; set; }              // userName (adicional, se necessário)
        public string UserNumber { get; set; }            // userNumber
        public string DesignationProduct { get; set; }    // designationProduct
        public string TelecomProvider { get; set; }       // telecomProvider
        public string FramingPlan { get; set; }           // framingPlan
        public string MigrationDevice { get; set; }       // migrationDevice (em validação)
        public string ServicePack { get; set; }           // servicePack (em validação)
        public string NewAreaCode { get; set; }           // newAreaCode
        public string NewUserNumber { get; set; }         // newUserNumber
        public string NewTelecomProvider { get; set; }    // newTelecomProvider
        public string CountryDateForRoaming { get; set; } // countryDateForRoaming
        public string TransactionID { get; set; }         // transactionID
    }
}
