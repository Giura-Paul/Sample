using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sample.DTO;

namespace Sample.App;

public static class HttpClientExtensions
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions {
        PropertyNameCaseInsensitive = true,
    };

    public static async Task<Result> SendAndGetResultAsync(this HttpClient client, HttpMethod method, Uri url, object? contentObj = null)
    {
        var result = await client.SendAndGetResultAsync<Result>(method, url, contentObj);
        return result.IsSuccess ? result.Data : Result.Error(result.ErrorMessage);
    }

    public static async Task<Result<T>> SendAndGetResultAsync<T>(this HttpClient client, HttpMethod method, Uri url, object? contentObj = null)
        where T : class
    {
        // No errors should happen here, so don't try/catch around this:
        var request = new HttpRequestMessage(method, url);

        if (contentObj != null)
        {
            string json = JsonSerializer.Serialize(contentObj);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response;

        // Here we might get an HttpRequestException, so check for that specific exception
        try
        {
            response = await client.SendAsync(request);
        }
        catch (HttpRequestException ex)
        {
            return Result<T>.Error($"Connection problem - cannot reach service ({ex.StatusCode}). Please try again.");
        }

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStreamAsync();

            try
            {
                var data = await JsonSerializer.DeserializeAsync<T>(content, _jsonOptions);

                if (data == null)
                    return Result<T>.Error("Missing result in response.");

                return Result<T>.Success(data);
            }
            catch (JsonException jsonEx)
            {
                return Result<T>.Error($"Json Error Caught: {jsonEx.Message}");
            }
        }
        else
        {
            return Result<T>.Error($"Unknown Error Encountered: {response.StatusCode}. Please try again later.");
        }
    }
}
