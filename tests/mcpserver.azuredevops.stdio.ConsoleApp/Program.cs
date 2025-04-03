using Microsoft.Extensions.Configuration;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;
using ModelContextProtocolServer.AzureDevops.Stdio.Tools;

var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
    {
        new("AZURE_DEVOPS_ORG_URL", "https://dev.azure.com/mstack"),
        new("AZURE_DEVOPS_PAT", Environment.GetEnvironmentVariable("MCP_PAT"))
    })
    .AddEnvironmentVariables()
    .Build();

var azureDevOpsClient = new AzureDevOpsClient(configuration);

var projectsTools = new ProjectTools(azureDevOpsClient);
var projects = await projectsTools.GetProjects(5);
Console.WriteLine(projects);

var gitTools = new GitTools(azureDevOpsClient);
var repositories = await gitTools.GetRepositories("38A58E48-A038-46D2-86C8-DFDC4A243A35");
var commits = await gitTools.GetCommitsForRepository("66752a17-f868-41c1-847a-38fe9812771d");