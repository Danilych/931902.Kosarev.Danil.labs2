var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRandomService, RandomNumberService>();
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
    int GetNum1();
    int GetNum2();
}

public class RandomNumberService : IRandomService
{
    int Num1;
    int Num2;

    public RandomNumberService()
    {
        Random random = new Random();
        Num1 = random.Next(0, 100);
        Num2 = random.Next(0, 100);
    }

    public int GetNum1()
    {
        return Num1;
    }

    public int GetNum2()
    {
        return Num2;
    }
}