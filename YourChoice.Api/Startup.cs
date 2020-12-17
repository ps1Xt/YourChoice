using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using YourChoice.Api.Database;
using YourChoice.Api.Hubs;
using YourChoice.Api.Hubs.Providers;
using YourChoice.Api.Infrastructure.Extensions;
using YourChoice.Api.Mappings;
using YourChoice.Api.Repositories.Implementation;
using YourChoice.Api.Repositories.Interfaces;
using YourChoice.Api.Services.implementation;
using YourChoice.Api.Services.Implementation;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain.Auth;

namespace YourChoice.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new PostProfile());
                m.AddProfile(new CommentProfile());
                m.AddProfile(new MessageProfile());
            });
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DataBaseContext>();


            var authOptions = services.ConfigureAuthOptions(Configuration);
            services.AddJwtAuthentication(authOptions);

            services.AddSingleton(mapperConfig.CreateMapper());
            ConfigureSwagger(services);
            services.AddDbContext<DataBaseContext>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {

                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin()
                          .WithExposedHeaders("Location");
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("default");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger API");

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifyHub>("/hubs/notify");
            });

        }
        private void ConfigureSwagger(IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = "Ivan Scoropad",
                Email = "vaneawar@gmail.com",
            };

            var license = new OpenApiLicense()
            {
                Name = "My License",
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Swagger Demo API",
                Description = "Swagger Api YourChoice",
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
            });
        }

    }
}
