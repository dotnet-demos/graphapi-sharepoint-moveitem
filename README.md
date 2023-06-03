# Demos how drive item can be moved between SharePoint Drives using Graph API via GraphSDK for .Net
The documentation of Graph API says the Patch on drive item cannot be used to move between drives. But it is working.

> When tried in different environments it failed when moving folder with large sized files in it. The exception was item not found. But behind the scene it really worked.

> :warning: Be cautious with undocumented features as Microsoft themselves cannot help, if we get stuck in production.

:white_check_mark: Workaround is to use the equivalent CSOM SharePoint APIs via [PnP SDKs](https://github.com/pnp/pnpcore).

# How to run
- Prepare SharePoint site
	- Create SharePoint Online tenant
	- Create user who has permission to the root site
	- Make sure the root site has 'Documents' and 'Archive' document libraries. Also referred as Drives
	- In the 'Documents' drive create a folder called 'tomove'. 
		- This folder will be moved to 'Archive' drive when program run
![Site setup](images/02-site-setup.png)
- Azure portal
	- Create Azure AD app registration and obtain the id. This needs to be replaced for the ClientId's value in appsettings.json
	- Provide the app registration required API Permissions. Screenshot from working environment below.
![APIPermissions](images/01-api-permissions.png)
- Application
	- Clone the repo
	- Replace the values in the appsettings.json
	- Run the console application. (Test application is yet to be done.)

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