using APIGateway.Endpoints;
using ApplicationService.Interfaces;
using ApplicationService.Implementations;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserService, UserService>();
//builder.Services.AddAuthentication().AddJwtBearer();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Setup endpoints
app.UseUserEndpoints();

app.Run();
