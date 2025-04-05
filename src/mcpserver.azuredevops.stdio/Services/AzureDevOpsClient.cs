using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using ModelContextProtocolServer.AzureDevops.Stdio.Services.Search;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestEase;
using Stef.Validation;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Services;

internal class AzureDevOpsClient
{
    public ProjectHttpClient ProjectClient { get; }

    public GitHttpClient GitClient { get; }

    public IAzureDevOpsSearchApi SearchApi { get; }

    public AzureDevOpsClient(IConfiguration configuration)
    {
        var baseUri = Guard.NotNullOrEmpty(configuration["AZURE_DEVOPS_ORG_URL"]);
        var auth = configuration["AZURE_DEVOPS_AUTH_METHOD"] ?? "pat";
        var pat = Guard.Condition(configuration["AZURE_DEVOPS_PAT"], s => !string.IsNullOrEmpty(s) && auth == "pat")!;

        var connection = new VssConnection(new Uri(baseUri), new VssBasicCredential(string.Empty, pat));

        ProjectClient = connection.GetClient<ProjectHttpClient>();
        GitClient = connection.GetClient<GitHttpClient>();
        SearchApi = GetIAzureDevOpsSearchApi(baseUri, pat);
    }

    private static IAzureDevOpsSearchApi GetIAzureDevOpsSearchApi(string baseUri, string pat)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        var searchApi = new RestClient(baseUri.Replace("https://dev.azure.com/", "https://almsearch.dev.azure.com/"))
        {
            JsonSerializerSettings = settings
        }.For<IAzureDevOpsSearchApi>();
        searchApi.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{pat}")));

        return searchApi;
    }
}