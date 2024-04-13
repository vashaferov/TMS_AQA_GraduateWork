using System.Text.Json;
using GraduateWork.Helpers.API;
using NLog;
using RestSharp;

namespace GraduateWork.Clients;

public class RestClientExtended
{
    private readonly RestClient _client;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public RestClientExtended()
    {
        var options = new RestClientOptions(Configurator.AppSettings.API_URL ?? throw new InvalidOperationException());

        _client = new RestClient(options);
        _client.AddDefaultHeaders(new Dictionary<string, string> { { "X-Api-Key", Configurator.AppSettings.ApiKey } });
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    private void LogRequest(RestRequest request)
    {
        _logger.Debug($"{request.Method} запрос: {request.Resource}");

        var body = request.Parameters
            .FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value;

        if (body != null)
        {
            _logger.Debug($"Тело запроса:\n{JsonSerializer.Serialize(body)}");
        }
    }

    private void LogResponse(RestResponse response)
    {
        if (response.ErrorException != null)
        {
            _logger.Error(
                $"Ошибка получения ответа:\n{response.ErrorException.Message}");
        }

        _logger.Debug($"Запрос завершился с кодом: {response.StatusCode}");

        if (!string.IsNullOrWhiteSpace(response.Content))
        {
            _logger.Debug($"Тело ответа:\n{response.Content}");
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