using System.ComponentModel;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class GitTools(AzureDevOpsClient azureDevOpsClient) : BaseTools
{
    [McpServerTool, Description("Retrieve git commits for a repository and project.")]
    public async Task<string> GetRepositories(
        [Description("The name or ID of the project.")] string projectId
    )
    {
        var repositories = await azureDevOpsClient.GitClient.GetRepositoriesAsync(projectId);
        return ToJson(repositories);
    }

    [McpServerTool, Description("Retrieve git commits for a repository.")]
    public async Task<string> GetCommitsForRepository(
        [Description("The ID of the repository.")] string repositoryId,
        [Description("Number of commits to return (default value is 100).")] int? top = null,
        [Description("Number of commits to skip.")] int? skip = null
    )
    {
        top ??= 100;

        var commits = await azureDevOpsClient.GitClient.GetCommitsAsync(repositoryId, new GitQueryCommitsCriteria(), skip, top);
        return ToJson(commits);
    }
}