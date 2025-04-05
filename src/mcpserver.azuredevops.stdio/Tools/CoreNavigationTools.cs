using System.ComponentModel;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class CoreNavigationTools(AzureDevOpsClient azureDevOpsClient)
{
    [McpServerTool, Description("Get Azure DevOps projects.")]
    public async Task<IReadOnlyList<TeamProjectReference>> GetProjects(
        [Description("Number of team projects to return.")] int? top = null,
        [Description("Number of team projects to skip.")] int? skip = null)
    {
        var allProjects = new List<TeamProjectReference>();

        string? continuationToken = null;
        do
        {
            var projects = await azureDevOpsClient.ProjectClient.GetProjects(top: top, skip: skip, continuationToken: continuationToken);
            allProjects.AddRange(projects);

            skip += projects.Count;

            if (top.HasValue && projects.Count < top)
            {
                continuationToken = projects.ContinuationToken;
            }
        }
        while (continuationToken != null);

        return allProjects;
    }

    [McpServerTool, Description("Retrieve git repositories.")]
    public async Task<IReadOnlyList<GitRepository>> GetRepositories(
        [Description("The name or ID of the project.")] string projectId
    )
    {
        return await azureDevOpsClient.GitClient.GetRepositoriesAsync(projectId);
    }
}