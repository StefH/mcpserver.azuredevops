using System.ComponentModel;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class RepositoryTools(AzureDevOpsClient azureDevOpsClient)
{
    [McpServerTool, Description("Retrieve details for a git repository.")]
    public Task<GitRepository?> GetRepository(
        [Description("The name or ID of the repository.")] string repositoryId
    )
    {
        return azureDevOpsClient.GitClient.GetRepositoryAsync(repositoryId);
    }
}