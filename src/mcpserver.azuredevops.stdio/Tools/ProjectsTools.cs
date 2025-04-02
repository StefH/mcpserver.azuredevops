using System.ComponentModel;
using Microsoft.TeamFoundation.Core.WebApi;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class ProjectsTools(AzureDevOpsClient azureDevOpsClient)
{
    [McpServerTool, Description("Get project with the specified id or name, optionally including capabilities.")]
    public Task<TeamProject> GetProject(
        [Description("The name or id of the project.")] string id,
        [Description("Include capabilities (such as source control) in the team project result.")] bool? includeCapabilities = null,
        [Description("Search within renamed projects (that had such name in the past).")] bool? includeHistory = null)
    {
        return azureDevOpsClient.ProjectClient.GetProject(id, includeCapabilities, includeHistory ?? false);
    }

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
}