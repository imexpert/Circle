using Circle.Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.DataAccess.Concrete.EntityFramework.Contexts
{
    public sealed class MsDbContext : ProjectDbContext
    {
        IHttpContextAccessor _httpContextAccessor;

        public const string DEFAULT_SCHEMA = "dbo";

        public MsDbContext(DbContextOptions<MsDbContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(options, configuration)
        {


            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CircleDbContext")));
            }

            var relationalOptions = RelationalOptionsExtension.Extract(optionsBuilder.Options);
            relationalOptions.WithMigrationsHistoryTableName("MigrationHistory");
            relationalOptions.WithMigrationsHistoryTableSchema(DEFAULT_SCHEMA);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                e.State == EntityState.Modified);

            var today = DateTime.Now;

            string userId = "AutomatedUser";
            string ipAdress = ":1";

            if (_httpContextAccessor.HttpContext != null &&
                _httpContextAccessor.HttpContext.Request != null &&
                _httpContextAccessor.HttpContext.Connection != null &&
                _httpContextAccessor.HttpContext.Connection?.RemoteIpAddress != null)
            {
                ipAdress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }

            if (_httpContextAccessor.HttpContext != null &&
                _httpContextAccessor.HttpContext?.User != null &&
                _httpContextAccessor.HttpContext?.User.Claims != null &&
                _httpContextAccessor.HttpContext?.User.Claims.Count() > 0)
            {
                userId = _httpContextAccessor.HttpContext?.User.Claims
                                .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
            }

            foreach (var entry in entries)
            {
                if (entry.Properties.Any(s => s.Metadata.Name == "Ip"))
                {
                    entry.Property("Ip").CurrentValue = ipAdress;
                }

                if (entry.State == EntityState.Added)
                {
                    if (entry.Properties.Any(s => s.Metadata.Name == "RecordUsername"))
                    {
                        entry.Property("RecordUsername").CurrentValue = userId;
                    }

                    if (entry.Properties.Any(s => s.Metadata.Name == "UpdateUsername"))
                    {
                        entry.Property("UpdateUsername").CurrentValue = userId;
                    }

                    if (entry.Properties.Any(s => s.Metadata.Name == "Id"))
                    {
                        Guid g = new Guid(entry.Property("Id").CurrentValue.ToString());

                        if (g == Guid.Empty)
                        {
                            entry.Property("Id").CurrentValue = Guid.NewGuid();
                        }
                    }

                    if (entry.Properties.Any(s => s.Metadata.Name == "RecordDate"))
                    {
                        entry.Property("RecordDate").CurrentValue = today;
                    }

                    if (entry.Properties.Any(s => s.Metadata.Name == "UpdateDate"))
                    {
                        entry.Property("UpdateDate").CurrentValue = today;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.GetType() == typeof(User) &&
                        userId == null &&
                        entry.Properties.Any(s => s.Metadata.Name == "UpdateUsername"))
                    {
                        entry.Property("UpdateUsername").CurrentValue = entry.Property("Email").CurrentValue;
                    }

                    if (entry.Properties.Any(s => s.Metadata.Name == "UpdateDate"))
                    {
                        entry.Property("UpdateDate").CurrentValue = today;
                    }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}