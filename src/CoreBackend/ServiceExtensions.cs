using CoreBackend.Features.auth;
using CoreBackend.Features.CalendarItems;
using CoreBackend.Features.Submissions;
using CoreBackend.Features.users;
using MongoConnection;
using MongoConnection.Collections.UserModel;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CoreBackend
{
    public static class ServiceExtensions
    {
        public static async Task<IServiceCollection> AddSingleTones(this IServiceCollection services, IConfigurationRoot configFile)
        {
            MongoSettings mongoSettings = configFile.GetRequiredSection(nameof(MongoSettings)).Get<MongoSettings>();
            MongoContext mongoContext = new MongoContext(mongoSettings);
            
            UserRepo userRepo = new UserRepo(mongoContext);

            User admin = new User();
            admin.PersonalNumber = "string"; 
            admin.Password = "string";
            admin.FirstName = "admin";
            admin.LastName = "admin";
            admin.AverageScore = 0;
            admin.BirthDate= new DateOnly();
            admin.IsFirstConnection = false;
            admin.MisbehaviorCount = 0;
            admin.Role= MongoConnection.Enums.UserRole.Admin;
            admin.TestScores= new Dictionary<string, int>();


            services.AddSingleton(mongoContext);
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<JwtService>();
            services.AddSingleton<ISubmissionsService, SubmissionsService>();
            services.AddSingleton<ICalendarItemsService, CalendarItemsService>();
            User? adminUser = await userRepo.GetByPNumAsync("string");
            
            if (adminUser == null)
                await userRepo.CreateAsync(admin);
            return services;
        }
    }
}
