# Demos how drive item can be moved between SharePoint Drives using Graph API via GraphSDK for .Net
The documentation of Graph API says the Patch on drive item cannot be used to move between drives. But it is working.

> When tried in different environments it failed when moving folder with large sized files in it. The exception was item not found. But behind the scene it really worked.

> :warning: Be cautious with undocumented features as Microsoft themselves cannot help, if we get stuck in production.

# Specifications

- .Net version - .Net 6
- Nugets referenced
	- Microsoft.Graph : v5.x (Code not compatible with v4.x)
	- DotNet.Helpers
	- easyconsolestd
	- Microsoft.Extensions.Hosting

# Dependency injection

- Supported. Refer the [Program.cs](/src/Program.cs) file for more details
- The options are injected as dependency to the [MenuService](/src/MenuService.cs then those are invoked based on selection. 