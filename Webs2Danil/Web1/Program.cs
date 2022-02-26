var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRandomService, RandomNumberSerice>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if(app.Environment.IsDevelopment())
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CalcService}/{action=Index}/{id?}");

app.Run();

public interface IRandomService
{
    int GetNumber();
}

public class RandomNumberSerice : IRandomService
{
    public int GetNumber()
    {
        Random random = new Random();
        return random.Next(0, 100);
    }
}