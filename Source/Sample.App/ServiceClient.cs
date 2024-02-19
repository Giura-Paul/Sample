using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.DTO;

namespace Sample.App;

internal class ServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseApiUrl;

    public ServiceClient()
    {
        _httpClient = new HttpClient();
        _baseApiUrl = "https://localhost:7117";
    }

    public async Task<Result<PersonDTO>> AddPersonAsync(PersonDTO personDTO)
    {
        var url = GetApiUrl("/api/people");

        return await _httpClient.SendAndGetResultAsync<PersonDTO>(HttpMethod.Post, url, personDTO);
    }

    public async Task<Result> DeletePersonAsync(int id)
    {
        var url = GetApiUrl($"/api/people/{id}");

        return await _httpClient.SendAndGetResultAsync(HttpMethod.Delete, url, id);
    }

    public async Task<Result<IEnumerable<PersonDTO>>> GetAllPersonsAsync()
    {
        var url = GetApiUrl("/api/people");

        return await _httpClient.SendAndGetResultAsync<IEnumerable<PersonDTO>>(HttpMethod.Get, url, null);
    }

    public async Task<Result> UpdatePersonAsync(int id, UpdatedPersonDTO updatedPersonDTO)
    {
        var url = GetApiUrl($"/api/people/{id}");

        return await _httpClient.SendAndGetResultAsync(HttpMethod.Put, url, updatedPersonDTO);
    }

    private Uri GetApiUrl(string path, params (string Name, object Value)[] queryStringParams)
    {
        var uri = new Uri(_baseApiUrl + path);

        if (queryStringParams.Length == 0)
            return uri;

        StringBuilder qs = new();

        bool first = true;

        foreach (var pair in queryStringParams.Where(p => p.Value != null))
        {
            if (!first)
                qs.Append('&');

            qs.Append(pair.Name);
            qs.Append('=');

            if (pair.Value is DateTime dateTime)
                qs.Append(new DateTime(dateTime.Ticks, DateTimeKind.Unspecified).ToString("O"));
            else
                qs.Append(pair.Value);

            first = false;
        }

        return new UriBuilder(uri) { Query = qs.ToString() }.Uri;
    }
}
