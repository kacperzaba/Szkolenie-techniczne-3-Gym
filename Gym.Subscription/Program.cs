using Gym.Subscription.Services;
using Gym.Subscription.Storage;
using System.Reflection;

namespace Gym.Subscription
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddDbContext<SubscriptionDbContext, SubscriptionDbContext>();
            builder.Services.AddSwaggerGen(c =>
            {
                var apiXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiPath = Path.Combine(AppContext.BaseDirectory, apiXml);
                c.IncludeXmlComments(apiPath);

                var crossCuttingXml = "Gym.Subscription.CrossCutting.xml";
                var crossCuttingPath = Path.Combine(AppContext.BaseDirectory, crossCuttingXml);
                c.IncludeXmlComments(crossCuttingPath);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
