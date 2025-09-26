namespace SIA.Domain.Exceptions
{
    public class ExternalProviderException(string provider, string message) : Exception($"External provider: {provider}, Error occurred: {message}");
}
