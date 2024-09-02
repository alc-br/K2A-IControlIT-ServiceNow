namespace IControlIT.Connect.Interfaces
{
    public interface IExternalIntegration
    {
        Task ProcessRequestAsync(string requestData);
        string SendResponse();
    }
}
