using System.ComponentModel;
using ModelContextProtocol.Server;
using ModelContextProtocolServer.AzureDevops.Stdio.Services;
using ModelContextProtocolServer.AzureDevops.Stdio.Services.Search;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Tools;

[McpServerToolType]
internal class SearchCodeTools(AzureDevOpsClient azureDevOpsClient)
{
    [McpServerTool, Description("Search for code across repositories in a project.")]
    public async Task<CodeSearchResponse> Search(
        [Description("Project ID or project name.")] string projectId,
        [Description("The search text.")] string searchText,
        [Description("Filter on projects (names).")] string[]? projects = null,
        [Description("Filter on repositories (names).")] string[]? repositories = null,
        [Description("Filter on branches.")] string[]? branches = null,
        [Description("Filter on paths.")] string[]? paths = null,
        [Description("Filter on code elements.")] string[]? codeElements = null,
        [Description("Flag to opt for faceting in the result.")] bool? includeFacets = null,
        [Description("Flag to opt for including matched code snippet in the result.")] bool? includeSnippet = null,
        [Description("Number of results to be returned.")] int? top = null,
        [Description("Number of results to be skipped.")] int? skip = null
    )
    {
        var searchRequest = new CodeSearchRequest
        {
            SearchText = searchText,
            IncludeFacets = includeFacets,
            IncludeSnippet = includeSnippet,
            Top = top ?? 100,
            Skip = skip,
            Filters = new Dictionary<string, List<string>>()
        };

        if (projects?.Length > 0)
        {
            searchRequest.Filters.Add("Project", projects.ToList());
        }

        if (repositories?.Length > 0)
        {
            searchRequest.Filters.Add("Repository", repositories.ToList());
        }

        if (branches?.Length > 0)
        {
            searchRequest.Filters.Add("Branch", branches.ToList());
        }

        if (paths?.Length > 0)
        {
            searchRequest.Filters.Add("Path", paths.ToList());
        }

        if (codeElements?.Length > 0)
        {
            searchRequest.Filters.Add("CodeElement", codeElements.ToList());
        }

        var response = await azureDevOpsClient.SearchApi.FetchCodeSearchResultsAsync(projectId, searchRequest);
        return response.GetContent();
    }
}