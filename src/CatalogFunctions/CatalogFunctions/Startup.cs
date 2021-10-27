using CatalogFunctions.Data;
using CatalogFunctions.Data.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(CatalogFunctions.Startup))]

namespace CatalogFunctions
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SQLServerCs");
            builder.Services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            /*
            CREATE TABLE [Products] (
                [Id] uniqueidentifier NOT NULL,
                [Title] nvarchar(max) NULL,
                [Description] nvarchar(max) NULL,
                [Price] float NOT NULL,
                [Quantity] int NOT NULL,
                [Status] int NOT NULL,
                [DateCreateAt] datetime2 NOT NULL,
                [DateDeleteAt] datetime2 NULL,
                CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
            );
            */
        }
    }
}
