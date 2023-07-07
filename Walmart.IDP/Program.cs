using Walmart.IDP;
using Serilog;
using Walmart.IDP.DbContexts;
using Microsoft.AspNetCore.Identity;
using Walmart.IDP.Entities;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<WalmartIdentityDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

    await dbContext.Database.MigrateAsync();
    await Seed.SeedRoles(roleManager);
    await Seed.SeedUsers(userManager);

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}