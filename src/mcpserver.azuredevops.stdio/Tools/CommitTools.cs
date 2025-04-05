using System.ComponentModel;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class CommitTools(AzureDevOpsClient azureDevOpsClient)
{
    [McpServerTool, Description("Retrieve a particular commit details for a repository.")]
    public Task<GitCommit> GetCommit(
        [Description("The ID of the commit.")] string commitId,
        [Description("The ID of the repository.")] string repositoryId,
        [Description("The number of changes to include in the result.")] int? changeCount = null
    )
    {
        return azureDevOpsClient.GitClient.GetCommitAsync(commitId, repositoryId, changeCount);
    }

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
}