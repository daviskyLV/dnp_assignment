using DNP1_Server.Database;
using DNP1_Server.Logic;

namespace DNP1_Server;
public class Program {
    public static IDatabase Database;
    public static ILoginLogic LoginLogic;
    
    public static void Main(string[] args) {
        // Setup
        Database = new Database.Database("users.json","posts.json");
        LoginLogic = new LoginLogic();

        // Template code
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}