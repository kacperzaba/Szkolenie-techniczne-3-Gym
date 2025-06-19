using Gym.FitnessClass.Resolvers;
using Gym.FitnessClass.Services;
using Gym.FitnessClass.Storage;

namespace Gym.FitnessClass
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

            builder.Services.AddScoped<IFitnessClassService, FitnessClassService>();
            builder.Services.AddDbContext<FitnessClassDbContext>();
            builder.Services.AddHttpClient<HttpCustomerResolver>();

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
