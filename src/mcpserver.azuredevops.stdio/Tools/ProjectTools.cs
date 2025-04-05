using System.ComponentModel;
using Microsoft.TeamFoundation.Core.WebApi;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class ProjectTools(AzureDevOpsClient azureDevOpsClient)
{
    [McpServerTool, Description("Get project details with the specified id or name, optionally including capabilities.")]
    public Task<TeamProject> GetProject(
        [Description("The name or id of the project.")] string id,
        [Description("Include capabilities (such as source control) in the team project result.")] bool? includeCapabilities = null,
        [Description("Search within renamed projects (that had such name in the past).")] bool? includeHistory = null)
    {
        return azureDevOpsClient.ProjectClient.GetProject(id, includeCapabilities, includeHistory ?? false);
    }
}