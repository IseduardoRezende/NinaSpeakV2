using NinaSpeakV2.Domain.Profiles;
using NinaSpeakV2.Domain.Services;
using NinaSpeakV2.Data.Repositories;
using NinaSpeakV2.Domain.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.IoC
{
    public static class NinaSpeakIoC
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IChatBotService, ChatBotService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IChatBotTypeService, ChatBotTypeService>();
            services.AddScoped<IChatBotGenreService, ChatBotGenreService>();
            services.AddScoped<IUserInstitutionService, UserInstitutionService>();
            services.AddScoped<IChatBotConversationService, ChatBotConversationService>();
            services.AddScoped<IChatBotUserInstitutionService, ChatBotUserInstitutionService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatBotRepository, ChatBotRepository>();
            services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            services.AddScoped<IChatBotTypeRepository, ChatBotTypeRepository>();
            services.AddScoped<IChatBotGenreRepository, ChatBotGenreRepository>();
            services.AddScoped<IUserInstitutionRepository, UserInstitutionRepository>();
            services.AddScoped<IChatBotConversationRepository, ChatBotConversationRepository>();
            services.AddScoped<IChatBotUserInstitutionRepository, ChatBotUserInstitutionRepository>();
        }

        public static void ConfigureAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(a =>
            {
                a.AddProfile<UserProfile>();
                a.AddProfile<ChatBotProfile>();
                a.AddProfile<InstitutionProfile>();
                a.AddProfile<ChatBotTypeProfile>();
                a.AddProfile<ChatBotGenreProfile>();
                a.AddProfile<UserInstitutionProfile>();
                a.AddProfile<ChatBotConversationProfile>();
                a.AddProfile<ChatBotUserInstitutionProfile>();
            });
        }
    }
}
