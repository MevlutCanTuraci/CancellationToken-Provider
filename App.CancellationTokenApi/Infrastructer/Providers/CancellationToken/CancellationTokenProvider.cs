namespace App.CancellationTokenApi.Infrastructer.Providers.CancellationToken;

public class CancellationTokenProvider : ICancellationTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CancellationTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    System.Threading.CancellationToken ICancellationTokenProvider.GetCancellationToken()
    {
        return (System.Threading.CancellationToken)_httpContextAccessor.HttpContext!.Items["CancellationToken"]!;
    }
}