using System.Diagnostics;
using GraduateWork.Helpers.API;
using NLog;
using RestSharp;
using RestSharp.Authenticators;

namespace GraduateWork.Clients;

public class RestClientExtended
{
    private readonly RestClient _client;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    
    public RestClientExtended()
    {
        var options = new RestClientOptions(Configurator.AppSettings.URL ?? throw new InvalidOperationException())
        {
            Authenticator =
                new HttpBasicAuthenticator(Configurator.AppSettings.Username, Configurator.AppSettings.Password)
        };
        
        _client = new RestClient(options);
    }
    
    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
    
    private void LogRequest(RestRequest request)
    {
        _logger.Debug($" {request.Method} request to: {request.Resource}");

        var body = request.Parameters
            .FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value;

        if (body != null)
        {
            _logger.Debug($" Тело запроса:\n{body}");
        }
    }
    
    private void LogResponse(RestResponse response)
    {
        if (response.ErrorException != null)
        {
            _logger.Error(
                $" Ошибка получения ответа:\n{response.ErrorException.Message}");
        }
        
        _logger.Debug($" Запрос завершился с кодом: {response.StatusCode}");

        if (!string.IsNullOrWhiteSpace(response.Content))
        {
            _logger.Debug($" Тело ответа:\n{response.Content}");
        }
    }
    
    public async Task<RestResponse> ExecuteAsync(RestRequest request)
    {
        LogRequest(request);
        var response = await _client.ExecuteAsync(request);
        LogResponse(response);
        
        return response;
    }

    public async Task<T> ExecuteAsync<T>(RestRequest request)
    {
        LogRequest(request);
        var response = await _client.ExecuteAsync<T>(request);
        LogResponse(response);

        return response.Data ?? throw new InvalidOperationException();
    }
}