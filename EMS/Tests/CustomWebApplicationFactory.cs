﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Data.Common;

namespace PubAppTest
{
    public partial class APITests
    {
        public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<Context>));

                    services.Remove(dbContextDescriptor);

                    var dbConnectionDescriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(Context));

                    services.Remove(dbConnectionDescriptor);

                    // Create open SqliteConnection so EF won't automatically close it.
                    services.AddSingleton<DbConnection>(container =>
                    {
                        var connection = new SqliteConnection("DataSource=:memory:");
                        connection.Open();

                        return connection;
                    });

                    services.AddDbContext<Context>((container, options) =>
                    {
                        var connection = container.GetRequiredService<DbConnection>();
                        options.UseSqlite(connection);
                    });
                });

                builder.UseEnvironment("Development");
            }
        }
    }
}