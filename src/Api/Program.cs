using Api;
using Application;
using Application.Identity;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //NameClaimType = ClaimTypes.NameIdentifier
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:admin-messages", policy =>
    {
        policy.Requirements.Add(new RbacRequirement("read:admin-messages"));
    });
});


builder.Services.AddSingleton<IAuthorizationHandler, RbacHandler>();


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());





app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
