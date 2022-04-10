﻿using AutoMapper;
using Pixel.Identity.Core;
using Pixel.Identity.Core.Conventions;
using Pixel.Identity.Store.Sql.Shared;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Pixel.Identity.Store.SqlServer;

/// <summary>
/// Configure Pixel Identity to use the MongoDb backend for asp.net identity and OpenIddict
/// </summary>
public class SqlConfigurator : IDataStoreConfigurator
{
    ///<inheritdoc/>
    public void ConfigureAutoMap(IServiceCollection services)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapProfile>();
        });

        #if DEBUG
        configuration.AssertConfigurationIsValid();
        #endif

        var mapper = configuration.CreateMapper();
        services.AddSingleton(mapper);
    }

    ///<inheritdoc/>
    public IdentityBuilder ConfigureIdentity(IConfiguration configuration, IServiceCollection services)
    {
        return services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));

            // Register the entity sets needed by OpenIddict.
            // Note: use the generic overload if you need
            // to replace the default OpenIddict entities.
            options.UseOpenIddict();
        })
        .AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.ClaimsIdentity.UserNameClaimType = Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = Claims.Role;
            options.SignIn.RequireConfirmedAccount = true;
        })
       .AddRoles<ApplicationRole>()
       .AddRoleStore<ApplicationRoleStore>()
       .AddUserStore<ApplicationUserStore>();
    }

    ///<inheritdoc/>
    public OpenIddictBuilder ConfigureOpenIdDictStore(IConfiguration configuration, OpenIddictBuilder builder)
    {
       return builder.AddCore(options =>
       {
           // Configure OpenIddict to use the Entity Framework Core stores and models.
           // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
           options.UseEntityFrameworkCore()
                  .UseDbContext<ApplicationDbContext>();

           // Enable Quartz.NET integration.
           options.UseQuartz();
       });
    }

    ///<inheritdoc/>
    public void AddServices(IServiceCollection services)
    {
        services.AddControllersWithViews()
            .AddApplicationPart(typeof(ApplicationUser).Assembly)
            .AddApplicationPart(typeof(SqlConfigurator).Assembly)
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.Add(new IdentityPageModelConvention<ApplicationUser, Guid>());
            }); ;                
        services.AddHostedService<Worker>();
    }
}
