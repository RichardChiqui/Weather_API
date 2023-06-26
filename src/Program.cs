using System.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.IgnoreNullValues = true;
});
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // add API routes
    endpoints.MapFallbackToFile("index.html"); // map all other requests to index.html
});

Debug.WriteLine("Request received for latitude {0} and longitude {1}");
//Debugger.Break();

//app.MapControllerRoute(
//   name: "default",
//  pattern: "{controller}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "weather",
    pattern: "/weather",
    defaults: new { controller = "Weather", action = "GetWeather", latitude = "", longitude = "" }
);

app.MapFallbackToFile("index.html"); ;

app.Run();
