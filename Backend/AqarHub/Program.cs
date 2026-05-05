using RealEstate.BL.Services;
using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RoleService>();


builder.Services.AddScoped<CityRepository>();
builder.Services.AddScoped<CityService>();


builder.Services.AddScoped<SqlHelper>(provider =>
    new SqlHelper("Server=localhost;Database=RealEstateDB;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<PropertyTypeRepository>();
builder.Services.AddScoped<PropertyTypeService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddScoped<PropertyService>();


builder.Services.AddScoped<FavoriteRepository>();
builder.Services.AddScoped<FavoriteService>();


builder.Services.AddScoped<DistrictRepository>();
builder.Services.AddScoped<DistrictService>();


builder.Services.AddScoped<PropertyImageRepository>();
builder.Services.AddScoped<PropertyImageService>();

builder.Services.AddScoped<UserSubscriptionRepository>();
builder.Services.AddScoped<UserSubscriptionService>();

builder.Services.AddScoped<PlanRepository>();
builder.Services.AddScoped<PlanService>();


builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<PaymentService>();

builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<MessageService>();

builder.Services.AddScoped<PropertyReviewRepository>();
builder.Services.AddScoped<PropertyReviewService>();

builder.Services.AddScoped<OwnerReviewRepository>();
builder.Services.AddScoped<OwnerReviewService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



