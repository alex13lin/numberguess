using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors (options => {
    options.AddPolicy ("All",
        builder => {
            builder.WithOrigins (new string[]{"https://stackblitz.com/"}).AllowAnyHeader().AllowCredentials()
                .AllowAnyMethod ();
        });
});
var app = builder.Build();

app.UseCors ("All");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "www")),
    RequestPath = "/www"
});

app.UseAuthorization();

app.MapControllers();

app.Run();
