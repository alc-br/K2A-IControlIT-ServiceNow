namespace IControlIT.Connect.Interfaces
{
    public interface IExternalIntegration
    {
        void ProcessRequest(string requestData);
        string SendResponse();
    }
}
