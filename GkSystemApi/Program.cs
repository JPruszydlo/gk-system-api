using gk_system_api.Entities;
using gk_system_api.Services;
using gk_system_api.Services.Interfaces;
using gk_system_api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "AllowSpecificOrigins";

// Add services to the container.
builder.Services.AddDbContext<GkSystemDbContext>();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        /*ValidIssuer = builder.Configuration["JwtSettings:Issuer"],*/
        ValidIssuers = new List<string>(builder.Configuration["JwtSettings:Issuer"].Split(',')),
        ValidAudiences = builder.Configuration.GetSection("JwtSettings:Audience").Get<string[]>(),
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(builder.Configuration["JwtSettings:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
    var a = builder.Configuration.GetSection("JwtSettings:Audience").Get<string[]>();
});
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(
                              "https://myshort.pl",
                              "https://gk-system.myshort.pl",
                              "https://admin.gk-system.myshort.pl",
                              "https://localhost:4200/"
                              ).AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true);
                          });

});

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    scope.ServiceProvider.GetService<DataSeeder>().SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();