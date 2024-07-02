using System.Reflection;
using SkillTest.Core.Domain.IRepository.Framwork;
using SkillTest.Core.Infrastructures.Repository.Framwork;
using SkillTest.Core.Repositories.Impl;
using MediatR;
using SkillTest.Core.Application.Services;
using SkillTest.Core.Application.Services.Impl;
using SkillTest.Core.IRepository;
using SkillTest.Core.Domain.IRepository;

namespace SelfieAWookie.API.UI.ExtentionMethods
{
    public static class DIMethods
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IAppUserRepository, AppUserRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static void AddInjection(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.Load("SkillTest.Core.Application"), Assembly.Load("SkillTest.Core.Domain"), Assembly.Load("SkillTest.API.UI"));
            services.AddAutoMapper(Assembly.Load("SkillTest.Core.Application"));

            // Service
            services.AddScoped<IAuthService, AuthService>();
            
        }

    }
}
