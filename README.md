# mcpserver.azuredevops
A Stdio MCP server as dotnet tool to access Azure DevOps.

## 📦 NuGet
[![NuGet Badge](https://img.shields.io/nuget/v/mcpserver.azuredevops.stdio)](https://www.nuget.org/packages/mcpserver.azuredevops.stdio)

### Installation
``` cmd
dotnet tool install --global mcpserver.azuredevops.stdio --version 0.0.1-preview-05
```

<br>

## 🛠️ Supported Tools

### Core Navigation Tools
- `GetProjects`: List all accessible projects
- `GetRepositories`: List all repositories in a project


### Project Tools
- `GetProject`: Get details of a specific project


### Repository Tools
- `GetRepository`: Get repository details


### Search Tools
- `SearchCode`: Search for code across repositories in a project


### GetCommit Tools
- `GetCommit`: Retrieve a particular commit details
- `GetCommits`: Retrieve git commits


### 🚧 Work Item Tools 🚧
- `get_work_item`: Retrieve a work item by ID
- `create_work_item`: Create a new work item
- `update_work_item`: Update an existing work item
- `list_work_items`: List work items in a project
- `manage_work_item_link`: Add, remove, or update links between work items


### 🚧 Boards 🚧
- `get_boards`: Get available boards in the project


### 🚧 Pipelines 🚧
- `list_pipelines`: List all pipelines in the project
- `trigger_pipeline`: Execute a pipeline


### 🚧 Pull Requests 🚧
- `list_pull_requests`: List pull requests
- `create_pull_request`: Create a new pull request
- `update_pull_request`: Update a pull request
- `get_pull_request`: Get pull request details


### 🚧 Wiki 🚧
- `get_wikis`: List all wikis in the project
- `get_wiki_page`: Get a wiki page
- `create_wiki`: Create a new wiki
- `update_wiki_page`: Create or update a wiki page


<br>

## 💡 LLM Examples

### Semantic Kernel
This MCP Server can be used in [ModelContextProtocol-SemanticKernel](https://github.com/StefH/McpDotNet.Extensions.SemanticKernel).

#### Question
``` raw
Get 5 commits from the repository '. . .'
```

#### Answer
``` raw
Here are the 3 most recent commits from the '. . .' repository:

1. **Commit ID:** [. . .](https://dev.azure.com/. . ./. . ./_git/. . ./commit/. . .)
   - **Author:** Stef Heyenrath
   - **Date:** 2021-12-17
   - **Comment:** Merged PR 378: Updated mindmap-certifications.json

2. **Commit ID:** [. . .](https://dev.azure.com/. . ./. . ./_git/. . ./commit/. . .)
   - **Author:** Stef Heyenrath
   - **Date:** 2021-12-17
   - **Comment:** Merge pull request 378 from stef-update-az104 into master

3. **Commit ID:** [. . .](https://dev.azure.com/. . ./. . ./_git/. . ./commit/. . .)
   - **Author:** Stef Heyenrath
   - **Date:** 2021-12-17
   - **Comment:** 103

If you need more details about any specific commit, feel free to ask!
```

### Claude Desktop
This MCP Server can also be used in Claude Desktop.

#### Config
``` json
{
    "mcpServers": {
        "azureDevOpsDotNet": {
            "command": "mcpserver.azuredevops.stdio",
            "args": [ ],
            "env": {
                "AZURE_DEVOPS_ORG_URL": "https://dev.azure.com/. . .",
                "AZURE_DEVOPS_AUTH_METHOD": "pat",
                "AZURE_DEVOPS_PAT": ". . .",
                "AZURE_DEVOPS_DEFAULT_PROJECT": "AzureExampleProjects"
            }
        }
    }
}
```

#### Question
Get 2 commits from the azure devops repository '. . .'.

#### Answer
![Claude Desktop-01](resources/screenshots/ClaudeDesktop-01.png)

<br>

## 📖 Links
- https://github.com/Tiberriver256/mcp-server-azure-devops
- https://github.com/stefanskiasan/azure-devops-mcp-server