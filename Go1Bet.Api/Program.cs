using Go1Bet.Infrastructure;
using Go1Bet.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Go1Bet.Core.Initializers;

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

////For google auth    https://gavilan.blog/2021/05/19/fixing-the-error-a-possible-object-cycle-was-detected-in-different-versions-of-asp-net-core/
//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
//Takes away the error. One to many
// https://stackoverflow.com/questions/59199593/net-core-3-0-possible-object-cycle-was-detected-which-is-not-supported
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Go1Bet API", Version = "v1" });
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
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
 //.WithOrigins(new[]�{�"http://localhost:3000"�})
  .SetIsOriginAllowed(origin => true)
  .AllowAnyHeader()
  .AllowCredentials()
  .AllowAnyMethod()
  );

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await IdentitiesInitializer.SeedIdentities(app);
await CateogoriesInitializer.SeedCategories(app);
await BonusesInitializer.SeedBonuses(app);

app.Run();
