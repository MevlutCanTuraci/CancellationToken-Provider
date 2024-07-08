using App.CancellationTokenApi.Infrastructer.Middlewares;
using App.CancellationTokenApi.Infrastructer.Providers.CancellationToken;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/* IOC register :: BEGIN */

//Add provider
builder.Services.AddTransient<ICancellationTokenProvider, CancellationTokenProvider>();
builder.Services.AddHttpContextAccessor(); //Add context accessor

/* IOC register :: END */


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


/* Add use custom middleware :: BEGIN */

app.UseMiddleware<CancellationTokenMiddleware>();

/* Add use custom middleware :: END */


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
