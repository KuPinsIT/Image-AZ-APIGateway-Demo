using ImageAZAPIGateway.DbMigrations;
using Microsoft.EntityFrameworkCore;
// The runner for applying migrations on target environment

using (var ctx = new ApplicationDbContextFactory().CreateDbContext([]))
{
    Console.WriteLine("Applying Migrations");

    await ctx.Database.MigrateAsync();

    Console.WriteLine("Finished!!!");
}