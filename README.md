# AssetTrack

# AsstApp.sln 
-> defines structure of .Net
-> contains reference to all projects
-> used by vscode to organize & build multiple project together

*** AssetApp  ***
-> Main application
# AssetApp.csproj
-> Project file that defines our .NET project
-> Specifies target framework(net 8.0), references types implicit usings
-> contains package reference and project settings

# Program.cs
-> Application entry point -> the main file
-> configures services (Razor components, interactive server components)
-> Set up HTTP request pipeline
-> Maps razor component with SSR

# appSettings.json && appsettings.Development.json
-> configuration files for different environment
-> allowed hosts
-> Development version overrides for local development

# launchSettings.json
-> Launch configuration for development
-> Defines how app runs (HTTP/HTTPS) port  env. variables


# Components Folder (UI Components)
# App.razor
-> Root HTML 

# Routes.razor
-> uses the route component to handle navigation

# _Imports.razor
-> Global using statements for all Razor components
-> Make these namespace available to all .razor files


# Layout Folder

# MainLayout.razor
-> Main page layout template
-> Defines the overall page structure
-> sidebar+ main context area
-> includes navigation menu and error handling UI
-> all pages use this layout by default

# xxx.razor.css
-> contains css of xxx file

# NavMenu.razor
-> Navigation menu component

# Page Folder
-> each xxx.razor files contains code of a specific route


# wwwroot
-> this folder contains static files
* app.css-> global application styles
* favicon.png-> website icon
* bootstrap/ -> bootstrap css framework

# Build Output Folders
* bin/Debug/net8.0/
-> compiled output from building the project
-> contains executable files and assemblies
* obj
-> Intermediate build files
-> some cache
-> auto-generated files that help with compilation process

*** How everything working ***
# Program.cs 
-> Starts the application and configures services
# App.razor 
-> Provides HTML shell
# Router.razor
-> Handles URL routing to specific pages
# MainLayout.razor
-> Wraps all pages with consistent layout 
# NavMenu.razor
-> Provides navigation between pages
# Page compnent
-> contains actual content
# Static files
-> images and some other assets


________________ *** _______________
