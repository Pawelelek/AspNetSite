using TopNewsApi.Infrastructure;
using TopNewsApi.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TopNewsApi.Infrastructure.Initializers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Create JWT Token Configuration
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]);
var tokenValidationParemeters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
    ValidAudience = builder.Configuration["JwtConfig:Audience"]
};

builder.Services.AddSingleton(tokenValidationParemeters);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParemeters;
    jwt.RequireHttpsMetadata = false;
});

// Create connection sting
string connStr = builder.Configuration.GetConnectionString("DefaultConnection");


// Database context
builder.Services.AddDbContext(connStr);

// Add Core services
builder.Services.AddCoreServices();

// Add Infrastructure Service
builder.Services.AddInfrastructureService();

// Add Repositories
builder.Services.AddRepositories();

// Add Mapping
builder.Services.AddMapping();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>

{

    option.SwaggerDoc("v1", new OpenApiInfo { Title = "TopNews API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme

    {

        In = ParameterLocation.Header,

        Description = "Please enter a valid token",

        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        BearerFormat = "JWT",

        Scheme = "Bearer"

    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement

 {

        {

            new OpenApiSecurityScheme

            {

                Reference = new OpenApiReference

                {

                    Type=ReferenceType.SecurityScheme,

                    Id="Bearer"

                }

            },

            new string[]{}

        }

 });

}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options

 ////.WithOrigins(new[]�{�"http://localhost:3000"�})

                .SetIsOriginAllowed(origin => true)

  .AllowAnyHeader()

  .AllowCredentials()

  .AllowAnyMethod()

  );

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


await UsersAndRolesInitializer.SeedUsersAndRoles(app);

app.Run();
