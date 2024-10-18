using Grpc.Net.Client;
using KoiOrderingSystem.APIService.Grpcs;
using KoiOrderingSystem.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(services =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:9876"); // Địa chỉ của gRPC server
    return new TravelGrpcService_.TravelGrpcService_Client(channel);
});

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<FA24_SE1717_PRN231_G3_KOIORDERINGSYSTEMINJAPANContext>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
