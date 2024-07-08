namespace App.CancellationTokenApi.Infrastructer.Providers.CancellationToken;

public interface ICancellationTokenProvider
{
    System.Threading.CancellationToken GetCancellationToken();
}