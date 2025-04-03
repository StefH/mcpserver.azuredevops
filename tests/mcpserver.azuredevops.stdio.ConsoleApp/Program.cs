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

var projectTools = new ProjectTools(azureDevOpsClient);
var projects = await projectTools.GetProjects(3);
Console.WriteLine(ToJson(projects));

var gitTools = new GitTools(azureDevOpsClient);
var repositories = await gitTools.GetRepositories(projects[1].Id.ToString());
Console.WriteLine(ToJson(repositories));

var commits = await gitTools.GetCommitsForRepository(repositories[0].Id.ToString());
Console.WriteLine(ToJson(commits));

return;

static string ToJson(object value)
{
    return JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
}