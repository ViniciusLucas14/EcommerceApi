using Ecommerce.Data.Context;
using Ecommerce.Data.Services;
using Ecommerce.Domain;
using Ecommerce.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
Ecommerce.Data.Startup.ConfigureService(builder.Services, builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EcommerceDbContext>(options =>
{
    string conn = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(conn);
});
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<Sti3Service>();

builder.Services.AddHttpClient();
builder.Services.AddApplication();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Can't connect to DB");
    }
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
