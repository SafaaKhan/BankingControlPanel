using BankingControlPanel_DataAccess.Data;
using BankingControlPanel_Models;
using BankingControlPanel_Models.Models;
using BankingControlPanel_Utilities.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_Utilities.Extensions
{
    public static class IdentityServiceExtension
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<ApplicationUser,ApplicationRole>(opt =>
            {
                // opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders(); 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,//search later
                        ValidateAudience = false//search later
                    };
                });
            services.AddScoped<TokenService>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(SD.RequireAdminRolePolicy, policy => policy.RequireRole(SD.AdminRole));
                opt.AddPolicy(SD.RequireUserRolePolicy, policy => policy.RequireRole(SD.AdminRole, SD.UserRole));
            });
            return services;
        }
    }
}
