using HefaWebhook.Models;
using HefaWebhook.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<SignalsDatabaseSettings>(
    builder.Configuration.GetSection("SignalsDatabase"));
builder.Services.AddSingleton<SignalsService>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SignaIrServer>("/SignaIrServer");
});

//app.MapHub<SignaIrServer>("/SignaIrServer");

app.Run();
