using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;
using ModelContextProtocolServer.AzureDevops.Stdio.Tools;

var configuration = new ConfigurationBuilder()
    //.AddInMemoryCollection(new List<KeyValuePair<string, string?>>
    //{
    //    new("AZURE_DEVOPS_ORG_URL", $"https://dev.azure.com/{Environment.GetEnvironmentVariable("AZURE_DEVOPS_ORG")}"),
    //    new("AZURE_DEVOPS_PAT", Environment.GetEnvironmentVariable("MCP_PAT"))
    //})
    .AddEnvironmentVariables()
    .Build();

var azureDevOpsClient = new AzureDevOpsClient(configuration);

var navTools = new CoreNavigationTools(azureDevOpsClient);
var projects = await navTools.GetProjects(3);
Console.WriteLine(ToJson(projects));

var repositories = await navTools.GetRepositories(projects[1].Id.ToString());
Console.WriteLine(ToJson(repositories));

var gitTools = new CommitTools(azureDevOpsClient);
var commits = await gitTools.GetCommitsForRepository(repositories[0].Id.ToString());
Console.WriteLine(ToJson(commits));

return;

static string ToJson(object value)
{
    return JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
}