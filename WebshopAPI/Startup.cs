using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Account.Commands.Delete;
using Webshop.Application.Account.Commands.Update;
using Webshop.Application.Account.Queries.GetAccountById;
using Webshop.Application.Cart.Commands.Add;
using Webshop.Application.Cart.Commands.Remove;
using Webshop.Application.Category.Queries.GetCategoryItems;
using Webshop.Application.Infrastructure;
using Webshop.Application.Item.Queries.GetItemById;
using Webshop.Application.Order.Commands.Create;
using Webshop.Application.Order.Commands.Pay;
using Webshop.Application.Order.Queries.GetOrder;
using Webshop.Application.Order.Queries.GetOrders;
using Webshop.Domain.Entities;
using Webshop.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.EntityFrameworkCore;
namespace WebshopAPI
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
            services.AddDbContext<WebshopContext>(options =>
                options.UseMySQL(
                    Configuration.GetConnectionString("Webshop_Content")).EnableSensitiveDataLogging());
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => { })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }); 
                services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Webshop API", Version = "v1" });
            });
            //Mediator pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<IValidator<CreateAccountCommand>, CreateAccountCommandValidator>();
            services.AddTransient<IValidator<DeleteAccountCommand>, DeleteAccountCommandValidator>();
            services.AddTransient<IValidator<UpdateAccountCommand>, UpdateAccountCommandValidator>();
            services.AddTransient<IValidator<GetAccountByIdQuery>, GetAccountByIdQueryValidator>();
            services.AddTransient<IValidator<AddItemToCartCommand>, AddItemToCartCommandValidator>();
            services.AddTransient<IValidator<RemoveItemFromCartCommand>, RemoveItemFromCartCommandValidator>();
            services.AddTransient<IValidator<GetCategoryItemsQuery>, GetCategoryItemsQueryValidator>();
            services.AddTransient<IValidator<GetItemByIdQuery>, GetItemByIdQueryValidator>();
            services.AddTransient<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
            services.AddTransient<IValidator<PayOrderCommand>, PayOrderCommandValidator>();
            services.AddTransient<IValidator<GetOrdersQuery>, GetOrdersQueryValidator>();
            services.AddTransient<IValidator<GetOrderQuery>, GetOrderQueryValidator>();
            services.AddMediatR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
           
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Webshop API V1");
            });

        }

        
    }
}
