﻿namespace API.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000");
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSignalR();

            return services;
        }
    }
}
