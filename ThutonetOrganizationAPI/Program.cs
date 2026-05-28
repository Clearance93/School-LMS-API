using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationModels;
using OrganizationUtility;
using OrganizationUtility.Sealed;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();    

SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                });

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddApplicationDependencies(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Apply any pending migrations to ensure the database exists and schema is up to date.
        try
        {
            logger.LogInformation("Applying pending migrations (if any)...");
            await db.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied.");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "An error occurred while applying database migrations. The DB may not exist or the connection is invalid.");
        }

        logger.LogInformation("Starting role seeding...");
        await RoleSeeder.SeedRolesAsync(roleManager, logger);
        logger.LogInformation("Role seeding completed successfully.");
    }
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    logger.LogError(ex, "An error occurred while seeding roles.");
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
