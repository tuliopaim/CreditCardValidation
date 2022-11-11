using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using CreditCardValidation.Commands;
using Microsoft.AspNetCore.WebUtilities;

namespace CreditCardValidation.Tests.IntegrationTests.Base;

public abstract class IntegrationTestBase
{
    public WebApplicationFactory<Program> GetSampleApplication()
    {
        return new WebApplicationFactory<Program>();
    }

    public virtual async Task<HttpResult<TResponse>> Post<TInput, TResponse>(
        HttpClient httpClient,
        string endpoint,
        TInput input) where TInput : class
    {
        var json = JsonConvert.SerializeObject(input);

        var response = await httpClient.PostAsync(
            endpoint,
            new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            return new(HttpStatusCode.InternalServerError, null, null);
        }

        var content = await response.Content.ReadAsStringAsync();

        CommandResponse<TResponse>? successResponse = null;
        ErrorCommandResponse? errorResponse = null;

        if (response.IsSuccessStatusCode)
        {
            successResponse = JsonConvert.DeserializeObject<CommandResponse<TResponse>>(content);
        }
        else
        {
            errorResponse = JsonConvert.DeserializeObject<ErrorCommandResponse>(content);
        }

        return new(response.StatusCode, successResponse, errorResponse);
    }

    public virtual async Task<HttpResult<TResponse>> Get<TResponse>(
        HttpClient httpClient,
        string endpoint,
        Dictionary<string, string?> queryParams) where TResponse : class
    {
        var uri = QueryHelpers.AddQueryString(endpoint, queryParams);

        var response = await httpClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            return new(HttpStatusCode.InternalServerError, null, null);
        }

        var content = await response.Content.ReadAsStringAsync();

        CommandResponse<TResponse>? successResponse = null;
        ErrorCommandResponse? errorResponse = null;

        if (response.IsSuccessStatusCode)
        {
            successResponse = JsonConvert.DeserializeObject<CommandResponse<TResponse>>(content);
        }
        else
        {
            errorResponse = JsonConvert.DeserializeObject<ErrorCommandResponse>(content);
        }

        return new(response.StatusCode, successResponse, errorResponse);
    }
}
