using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using BlazorDemo.Core.Interfaces;
using BlazorDemo.Infrastructure.Data;
using BlazorDemo.Server.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("RushAgDb");
builder.Services.AddDbContext<BlazorDemoDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepository,Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.AddTodoApis();

app.MapRazorPages();
//app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
