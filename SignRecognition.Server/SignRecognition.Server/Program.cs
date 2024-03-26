using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SignRecognition.Domain.Configurations;
using SignRecognition.Repository;
using SignRecognition.Server.Controllers;
using SignRecognition.Server.Module;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers(options=> 
    options.InputFormatters.Add(new ByteArrayInputFormatter()));
// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServices();
builder.Services.AddRepositories();

var jwtConfiguration = builder.Configuration.GetSection("JwtToken").Get<JwtTokenConfiguration>();
builder.Services.AddSingleton(jwtConfiguration);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var key = Encoding.UTF8.GetBytes(jwtConfiguration.Key);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfiguration.Issuer,
        ValidAudience = jwtConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "SignRecognition.Server", Version = "v1"});
                
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            System.Array.Empty<string>()
        }
    });
});

var  corspolicy = "_corsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corspolicy,
        policy  =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(corspolicy);

app.Run();