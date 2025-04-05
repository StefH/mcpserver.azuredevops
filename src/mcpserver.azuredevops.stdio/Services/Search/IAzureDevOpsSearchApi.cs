using System.Net.Http.Headers;
using Newtonsoft.Json;
using RestEase;

namespace ModelContextProtocolServer.AzureDevops.Stdio.Services.Search
{
    /// <summary>
    /// Interface for Azure DevOps Search API.
    /// </summary>
    public interface IAzureDevOpsSearchApi
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        /// <summary>
        /// Fetches code search results from Azure DevOps.
        /// </summary>
        /// <param name="project">Project ID or project name.</param>
        /// <param name="request">The code search request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the code search response.</returns>
        [Post("{project}/_apis/search/codesearchresults?api-version=7.1")]
        Task<Response<CodeSearchResponse>> FetchCodeSearchResultsAsync(
            [Path] string project,
            [Body] CodeSearchRequest request
        );
    }

    /// <summary>
    /// Defines a code search request.
    /// </summary>
    public class CodeSearchRequest
    {
        /// <summary>
        /// Options for sorting search results.
        /// If set to null, the results will be returned sorted by relevance.
        /// If more than one sort option is provided, the results are sorted in the order specified in the OrderBy.
        /// </summary>
        [JsonProperty("$orderBy")]
        public List<SortOption>? OrderBy { get; set; }

        /// <summary>
        /// Number of results to be skipped.
        /// </summary>
        [JsonProperty("$skip")]
        public int? Skip { get; set; }

        /// <summary>
        /// Number of results to be returned.
        /// </summary>
        [JsonProperty("$top")]
        public int? Top { get; set; }

        /// <summary>
        /// Filters to be applied.
        /// </summary>
        public Dictionary<string, List<string>>? Filters { get; set; }

        /// <summary>
        /// Flag to opt for faceting in the result.
        /// </summary>
        public bool? IncludeFacets { get; set; }

        /// <summary>
        /// Flag to opt for including matched code snippet in the result.
        /// </summary>
        public bool? IncludeSnippet { get; set; }

        /// <summary>
        /// The search text.
        /// </summary>
        public required string SearchText { get; init; }
    }

    /// <summary>
    /// Defines a code search response item.
    /// </summary>
    public class CodeSearchResponse
    {
        /// <summary>
        /// Total number of matched files.
        /// </summary>
        public required int Count { get; init; }

        /// <summary>
        /// List of matched files.
        /// </summary>
        public required List<CodeResult> Results { get; init; }

        /// <summary>
        /// Numeric code indicating any additional information:
        /// 0 - Ok
        /// 1 - Account is being reindexed
        /// 2 - Account indexing has not started
        /// 3 - Invalid Request
        /// 4 - Prefix wildcard query not supported
        /// 5 - MultiWords with code facet not supported
        /// 6 - Account is being onboarded
        /// 7 - Account is being onboarded or reindexed
        /// 8 - Top value trimmed to max result allowed
        /// 9 - Branches are being indexed
        /// 10 - Faceting not enabled
        /// 11 - Work items not accessible
        /// 19 - Phrase queries with code type filters not supported
        /// 20 - Wildcard queries with code type filters not supported.
        ///
        /// Any other info code is used for internal purpose.
        /// </summary>
        public required int InfoCode { get; init; }

        /// <summary>
        /// A dictionary storing an array of Filter object against each facet.
        /// </summary>
        public required Dictionary<string, List<Facet>> Facets { get; init; }
    }

    /// <summary>
    /// Defines the code result containing information of the searched files and its metadata.
    /// </summary>
    public class CodeResult
    {
        /// <summary>
        /// Name of the result file.
        /// </summary>
        public required string FileName { get; init; }

        /// <summary>
        /// Path at which result file is present.
        /// </summary>
        public required string Path { get; init; }

        /// <summary>
        /// Dictionary of field to hit offsets in the result file.
        /// </summary>
        public required Dictionary<string, List<Match>> Matches { get; init; }

        /// <summary>
        /// Collection of the result file.
        /// </summary>
        public required Collection Collection { get; init; }

        /// <summary>
        /// Project of the result file.
        /// </summary>
        public required Project Project { get; init; }

        /// <summary>
        /// Repository of the result file.
        /// </summary>
        public required Repository Repository { get; init; }

        /// <summary>
        /// Versions of the result file.
        /// </summary>
        public required List<Version> Versions { get; init; }

        /// <summary>
        /// ContentId of the result file.
        /// </summary>
        public required string ContentId { get; init; }
    }

    /// <summary>
    /// Defines the details of the collection.
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Name of the collection.
        /// </summary>
        public required string Name { get; init; }
    }

    /// <summary>
    /// Defines the details of the project.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Id of the project.
        /// </summary>
        public required string Id { get; init; }

        /// <summary>
        /// Name of the project.
        /// </summary>
        public required string Name { get; init; }
    }

    /// <summary>
    /// Defines the details of the repository.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Id of the repository.
        /// </summary>
        public required string Id { get; init; }

        /// <summary>
        /// Name of the repository.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Version control type of the result file.
        /// </summary>
        public required string Type { get; init; }
    }

    /// <summary>
    /// Defines how to sort the result.
    /// </summary>
    public class SortOption
    {
        /// <summary>
        /// Field name on which sorting should be done.
        /// </summary>
        public required string Field { get; init; }

        /// <summary>
        /// Order (ASC/DESC) in which the results should be sorted.
        /// </summary>
        public required string SortOrder { get; init; }
    }

    /// <summary>
    /// Describes the details pertaining to a version of the result file.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Name of the branch.
        /// </summary>
        public required string BranchName { get; init; }

        /// <summary>
        /// ChangeId in the given branch associated with this match.
        /// </summary>
        public required string ChangeId { get; init; }
    }

    /// <summary>
    /// Defines a match in the result file.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Character offset of the match.
        /// </summary>
        public required int CharOffset { get; init; }

        /// <summary>
        /// Length of the match.
        /// </summary>
        public required int Length { get; init; }
    }

    /// <summary>
    /// Defines a facet in the search result.
    /// </summary>
    public class Facet
    {
        /// <summary>
        /// Name of the facet.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Id of the facet.
        /// </summary>
        public required string Id { get; init; }

        /// <summary>
        /// Result count for the facet.
        /// </summary>
        public required int ResultCount { get; init; }
    }
}
