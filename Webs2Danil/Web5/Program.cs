using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web5.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Web5Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Web5Context") ?? throw new InvalidOperationException("Connection string 'Web5Context' not found.")));

builder.Services.AddControllersWithViews();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();

