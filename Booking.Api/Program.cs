using Booking.Api.ApiHandlers.Hotels;
using Booking.Api.ApiHandlers.Reservations;
using Booking.Api.ApiHandlers.Rooms;
using Booking.Api.Filters;
using Booking.Api.Middleware;
using Booking.Infrastructure.DataSource;
using Booking.Infrastructure.Extensions;
using Booking.Infrastructure.Mailing;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

namespace Booking.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var config = builder.Configuration;

                builder.Host.UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.WriteTo.Console();
                    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
                });

                builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

                builder.Services.AddDbContext<DataContext>(opts =>
                {
                    opts.UseSqlServer(config.GetConnectionString("db"));
                });

                builder.Services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));

                // Add services to the container.
                builder.Services.AddAuthorization();
                builder.Services.AddDomainServices();
                builder.Services.AddApplicationServices();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddMediatR(Assembly.Load("Booking.Application"), typeof(Program).Assembly);                

                var app = builder.Build();

                app.UseSerilogRequestLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }            

                app.UseAuthorization();

                app.UseMiddleware<AppExceptionHandlerMiddleware>();

                app.MapGroup("/api/hotels").MapHotels().AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
                app.MapGroup("/api/rooms").MapRooms().AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
                app.MapGroup("/api/reservations").MapReservations().AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

                app.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "server terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
