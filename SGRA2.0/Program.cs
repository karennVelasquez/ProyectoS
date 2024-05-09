using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Repositories;
using SGRA2._0.Service;
using static SGRA2._0.Repositories.IAchievementsGamesRespositories;
using static SGRA2._0.Repositories.IRecordTimeRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<PersonDBContext>(options => options.UseSqlServer(conString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AgriculturalWasteManagementSystems",
        Version = "v1"
    });
});*/

builder.Services.AddScoped<IAchievementsGamesRespositories, AchievementsGamesRespositories>();
builder.Services.AddScoped<IAchievementsGamesService, AchievementsGamesService>();

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

builder.Services.AddScoped<IScoreRepositories, ScoreRepositories>();
builder.Services.AddScoped<IScoreService,  ScoreService>();

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

//Configuracion CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins(
                "http://localhost:3000/"
                )
            .AllowAnyMethod()
            .AllowAnyHeader();
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

//
app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.Run();
