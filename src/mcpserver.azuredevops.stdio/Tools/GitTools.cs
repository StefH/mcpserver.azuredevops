using System.ComponentModel;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class GitTools(AzureDevOpsClient azureDevOpsClient)
{
    [McpServerTool, Description("Retrieve git commits for a repository.")]
    public async Task<IReadOnlyList<GitCommitRef>> GetCommitsForRepository(
        [Description("The ID of the repository.")] string repositoryId,
        [Description("Number of commits to return (default value is 100).")] int? top = null,
        [Description("Number of commits to skip.")] int? skip = null
    )
    {
        top ??= 100;

        return await azureDevOpsClient.GitClient.GetCommitsAsync(repositoryId, new GitQueryCommitsCriteria(), skip, top);
    }

    [McpServerTool, Description("Retrieve a git repository.")]
    public Task<GitRepository?> GetRepository(
        [Description("The name or ID of the repository.")] string repositoryId
    )
    {
        return azureDevOpsClient.GitClient.GetRepositoryAsync(repositoryId);
    }

    [McpServerTool, Description("Retrieve git commits for a repository and project.")]
    public async Task<IReadOnlyList<GitRepository>> GetRepositories(
        [Description("The name or ID of the project.")] string projectId
    )
    {
        return await azureDevOpsClient.GitClient.GetRepositoriesAsync(projectId);
    }
}