using App.Application.Abstractions.Token;
using App.Application.Profiles;
using App.Application.UnitOfWorks;
using App.Domain.Entitites;
using App.Infrastructure.Services.Email;
using App.Infrastructure.Services.Token;
using App.Persistence.Contexts;
using App.Persistence.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddCors(x => x.AddDefaultPolicy(policy => policy.WithOrigins("https://avonnet.az", "https://www.avonnet.az", "https://avonnet.az", "http://avonnet.az", "https://expresskuryer.az", "http://localhost:5000", "https://humanpower.az", "http://humanpower.az", "http://localhost:8000", "https://localhost:8000", "http://127.0.0.1:8000", "https://127.0.0.1:8000").AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddIdentity<AppUser,IdentityRole>(opt=>
{
	opt.Password.RequireDigit = false;
	opt.Password.RequireLowercase = false;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequiredLength = 6;
	opt.Password.RequiredUniqueChars = 0;
	opt.Password.RequireUppercase = false;
	opt.Lockout.AllowedForNewUsers = false;
	opt.Lockout.MaxFailedAccessAttempts = 70;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);

}).AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<IEmailService, EmailService>();
var mapperConfig = new MapperConfiguration(mc =>
{
	mc.AddProfile(new MapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
