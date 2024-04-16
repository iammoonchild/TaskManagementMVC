
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Repositories.Repositories;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbTaskManagementContext>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();
builder.Services.AddScoped<IManagerRepo, ManagerRepo>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IJWTService, JWTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
