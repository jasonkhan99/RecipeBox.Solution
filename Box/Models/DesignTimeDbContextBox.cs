using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Box.Models
{
  public class BoxContextBox : IDesignTimeDbContextBox<BoxContext>
  {
    BoxContext IDesignTimeDbContextBox<BoxContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
      .Build();

      var builder = new DbContextOptionsBuilder<BoxContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);
      
      return new BoxContext(builder.Options);
    }
  }
}