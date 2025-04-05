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
var commits = await gitTools.GetCommits(repositories[1].Id.ToString());
Console.WriteLine(ToJson(commits));

var searchTools = new SearchCodeTools(azureDevOpsClient);
var search = await searchTools.Search(
    projects[1].Id.ToString(), 
    "async",
    projects: [projects[1].Name],
    repositories: [repositories[1].Name],
    codeElements: ["class"],
    includeSnippet: true,
    top: 5
);
Console.WriteLine(ToJson(search));
return;

static string ToJson(object value)
{
    return JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
}