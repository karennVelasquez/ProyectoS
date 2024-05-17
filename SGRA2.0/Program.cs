using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Repositories;
using SGRA2._0.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using static SGRA2._0.Repositories.IRecordTimeRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using SGRA2._0.Helpers;


var builder = WebApplication.CreateBuilder(args);
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<PersonDBContext>(options => options.UseSqlServer(conString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User"));
});

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Organic agricultural waste management system",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer hsgda56hbd\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});

builder.Services.AddScoped<IAchievementsRepositories,  AchievementsRepositories>();
builder.Services.AddScoped<IAchievementsService,  AchievementsService>();   

builder.Services.AddScoped<IChemicalCompositionRepositories,  ChemicalCompositionRepositories>();
builder.Services.AddScoped<IChemicalCompositionService,  ChemicalCompositionService>();

builder.Services.AddScoped<ICollectWasteRepositories,  CollectWasteRepositories>();
builder.Services.AddScoped<ICollectWasteService, CollectWasteService>();

builder.Services.AddScoped<IComposterRepositories, ComposterRepositories>();
builder.Services.AddScoped<IComposterService, ComposterService>();

builder.Services.AddScoped<ICustomerRepositories, CustomerRepositories>();
builder.Services.AddScoped<ICustomerService,  CustomerService>();

builder.Services.AddScoped<IDocumentTypeRepositories, DocumentTypeRepositories>();  
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();

builder.Services.AddScoped<IEmployeeRepositories, EmployeeRepositories>();
builder.Services.AddScoped<IEmployeeService,  EmployeeService>();

builder.Services.AddScoped<IFinalCompostRepositories, FinalCompostRepositories>();
builder.Services.AddScoped<IFinalCompostService,  FinalCompostService>();

builder.Services.AddScoped<IFlipRepositories, FlipRepositories>();
builder.Services.AddScoped<IFlipService,  FlipService>();

builder.Services.AddScoped<IGamesRepositories,  GamesRepositories>();
builder.Services.AddScoped<IGamesService,  GamesService>(); 

builder.Services.AddScoped<ILevelRepositories,  LevelRepositories>();
builder.Services.AddScoped<ILevelService,  LevelService>();

builder.Services.AddScoped<IPersonRepository, PersonRepositories>();
builder.Services.AddScoped<IPersonService,  PersonService>();

builder.Services.AddScoped<IProcessStageRepositories,  ProcessStageRepositories>();
builder.Services.AddScoped<IProcessStageService,  ProcessStageService>();

builder.Services.AddScoped<IRecordTimeRepositories, RecordTimeRepositories>();
builder.Services.AddScoped<IRecordTimeService,  RecordTimeService>();

builder.Services.AddScoped<ISaleRepositories,  SaleRepositories>(); 
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddScoped<ISuppliersRepositories, SuppliersRepositories>();
builder.Services.AddScoped<ISuppliersService,  SuppliersService>(); 

builder.Services.AddScoped<ITemperatureRepositories,  TemperatureRepositories>();   
builder.Services.AddScoped<ITemperatureService,  TemperatureService>(); 

builder.Services.AddScoped<ITimeRepositories, TimeRepositories>(); 
builder.Services.AddScoped<ITimeService, TimeService>();

builder.Services.AddScoped<ITransactionRepositories, TransactionRepositories>();
builder.Services.AddScoped<ITransactionService,  TransactionService>();

builder.Services.AddScoped<IUserRepositories,  UserRepositories>(); 
builder.Services.AddScoped<IUserService,  UserService>();

builder.Services.AddScoped<IWasteRepositories,  WasteRepositories>();   
builder.Services.AddScoped<IWasteService,  WasteService>();

builder.Services.AddScoped<IWasteTypeRepositories,  WasteTypeRepositories>(); 
builder.Services.AddScoped<IWasteTypeService,  WasteTypeService>();

builder.Services.AddScoped<IPersonLoginRepository, PersonLoginRepositories>();
builder.Services.AddScoped<IPersonLoginService, PersonLoginService>();

//
var key = builder.Configuration.GetValue<string>("Jwt:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

//CORS SI FUNCIONA
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
// Agrega el middleware de CORS al pipeline de solicitud HTTP
app.UseCors("NuevaPolitica");
//Autenticacion
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
