using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskMangment.Api.Authentications;
using TaskMangment.Api.Mapping;
using TaskMangment.Api.Middlewares;
using TaskMangment.Buisness.Services.STask;
using TaskMangment.Buisness.Services.SUser;
using TaskMangment.Data;
using TaskMangment.Data.Repositories.RTask;
using TaskMangment.Data.Repositories.RUser;
using DotNetEnv;

Env.Load();
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var jwtOptions = new JwtOptions
{
    Secret = Environment.GetEnvironmentVariable("JWT__SECRET")!,
    Issuer = Environment.GetEnvironmentVariable("JWT__ISSUER")!,
    Audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")!,
    ExpirationInMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT__EXPIRATIONINMINUTES")!)
};
builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, 
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };
    });
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ToDoListDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRINGS__DEFAULTCONNECTION");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddAutoMapper(typeof(TaskProfile));
var app = builder.Build();
app.UseMiddleware<ValidateIdMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/openapi/v1.json","TaskMangment.Api"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
