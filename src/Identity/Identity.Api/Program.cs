var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); 

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();