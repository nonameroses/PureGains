using Api;
using Application;
using Application.Identity;
using Domain.Entities.Identity;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = "https://dev-65pswrm4no6me7lw.uk.auth0.com/";
//    options.Audience = "https://puregain.com";
//});


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

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
//    options.AddPolicy("read:getusers", policy => policy.Requirements.Add(new HasScopeRequirement("read:getusers", domain)));
//});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:admin-messages", policy =>
    {
        policy.Requirements.Add(new HasScopeRequirement("read:admin-messages", domain));
    });
});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new
//        HasScopeRequirement("read:messages", domain)));
//    options.AddPolicy("read:admin-messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:admin-messages", domain)));
//});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


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
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());





app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
