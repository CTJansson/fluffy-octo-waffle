
namespace api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://demo.duendesoftware.com";//"https://localhost:5002";
                options.Audience = "api";
                options.RequireHttpsMetadata = false; // Use true in production
            });


        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiCaller", policy =>
            {
                policy.RequireClaim("scope", "api");
            });

            options.AddPolicy("InteractiveUser", policy =>
            {
                policy.RequireClaim("sub");
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers().RequireAuthorization();

        app.Run();
    }
}
