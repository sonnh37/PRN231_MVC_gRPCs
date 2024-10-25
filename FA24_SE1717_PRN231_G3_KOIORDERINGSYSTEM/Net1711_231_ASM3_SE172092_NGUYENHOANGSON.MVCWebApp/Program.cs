using Grpc.Net.Client;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Grpcs;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(services =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:9876"); // Địa chỉ của gRPC server
    return new BookingRequestGrpcService_.BookingRequestGrpcService_Client(channel);
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
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();