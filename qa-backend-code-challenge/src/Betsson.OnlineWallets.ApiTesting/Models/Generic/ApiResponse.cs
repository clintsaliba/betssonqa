namespace Betsson.OnlineWallets.ApiTesting.Models.Generic
{
    public class ApiResponse<TSuccess, TError>
    {
        public TSuccess Success { get; set; }
        public TError Error { get; set; }
    }

}