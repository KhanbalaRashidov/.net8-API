using CompanyEmpolyees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
app.UseAuthorization();
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("My Custom Middleware component");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello from the middleware component.\n");
//    //Console.WriteLine($"Logic before executing the next delegate in the Use method");
//    await next.Invoke();
//    Console.WriteLine($"Logic after executing the next delegate in the Use method");
//});
//app.Run(async context =>
//{
//    Console.WriteLine($"Writing the response to the client in the Run method");
//    context.Response.StatusCode = 200;
//    await context.Response.WriteAsync("Hello from the middleware component.");
//});

app.MapControllers();

app.Run();
