using Gym.Client.Resolvers;
using Gym.Client.Services;
using Gym.Client.Storage;

namespace Gym.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddDbContext<ClientDbContext, ClientDbContext>();
            builder.Services.AddHttpClient<HttpSubscriptionResolver>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
