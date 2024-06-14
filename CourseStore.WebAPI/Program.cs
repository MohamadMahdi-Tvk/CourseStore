using CourseStore.BLL.Tags.Commands;
using CourseStore.DAL.DbContexts;
using CourseStore.DAL.Framework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<CourseStoreDbContext>(c => c.UseSqlServer("Server=.; Initial Catalog=WorkShopDb; Integrated Security=true; TrustServerCertificate=true")
.AddInterceptors(new AddAuditFieldInterceptor()));

//builder.Services.AddMediatR(typeof(CreateTagHandler).Assembly);
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateTagHandler).GetTypeInfo().Assembly));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
