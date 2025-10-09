using AssetApp.Components;
using AssetApp.Services;
using AssetApp.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<DashboardService>();


builder.Services.AddScoped<Session>();                // Session handler
builder.Services.AddSingleton<AuthService>();         // Single-user validation
builder.Services.AddScoped<ProtectedSessionStorage>(); // Required for ProtectedSessionStorage

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 🧩 Map your Razor components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
