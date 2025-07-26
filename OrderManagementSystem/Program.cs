using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace OrderManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrderDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(AssemblyReferenceProfiles).Assembly);

            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
