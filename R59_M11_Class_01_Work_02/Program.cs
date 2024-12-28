using Microsoft.EntityFrameworkCore;
using R59_M11_Class_01_Work_02.Models_;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DeviceDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddCors(op => op.AddPolicy("EnableCors", p =>
{
    p.WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();
app.UseStaticFiles();
app.UseCors("EnableCors");
app.MapDefaultControllerRoute();


app.Run();
