var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Quiz.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.IsEssential = true;
}) ;


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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=QuizService}/{action=Index}/{id?}");



string CalcResult(int numOne, int numTwo, int operType)
{
    if (operType == 0)
    {
        return (numOne + numTwo).ToString();
    }
    else if (operType == 1)
    {
        return (numOne - numTwo).ToString();
    }
    else if (operType == 2)
    {
        return (numOne * numTwo).ToString();
    }
    else
    {
        return ((numTwo != 0) ? (numOne / numTwo).ToString() : "Error (division on 0)");
    }
}

app.MapGet("/api/opers/{numone:int}/{numtwo:int}/{opertype:int}", (int numone, int numtwo, int opertype) =>
{
    return CalcResult(numone, numtwo, opertype);
});

app.MapGet("/api/opers/plus/{numone:int}/{numtwo:int}", (int numone, int numtwo) =>
{
    return  numone + numtwo;
});

app.MapGet("/api/opers/minus/{numone:int}/{numtwo:int}", (int numone, int numtwo) =>
{
    return numone - numtwo;
});

app.MapGet("/api/opers/multiply/{numone:int}/{numtwo:int}", (int numone, int numtwo) =>
{
    return (numone * numtwo).ToString();
});

app.MapGet("/api/opers/divide/{numone:int}/{numtwo:int}", (int numone, int numtwo) =>
{
    return ((numtwo != 0) ? (numone / numtwo).ToString() : "Error (division on 0)");
});

app.Run();

