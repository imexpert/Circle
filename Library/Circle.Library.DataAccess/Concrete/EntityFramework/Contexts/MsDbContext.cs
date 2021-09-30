using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || 
                e.State == EntityState.Modified);

            var today = DateTime.Now;

            var userId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

            
            var ip = _httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;

            foreach (var entry in entries)
            {
                if (entry.Property("Ip") != null)
                {
                    entry.Property("Ip").CurrentValue = ip.ToString();
                }

                if (entry.State == EntityState.Added)
                {
                    if (userId == null)
                    {
                        throw new Exception("Kullanıcı adı alınamadı.");
                    }

                    if (entry.Property("RecordUsername") != null)
                    {
                        entry.Property("RecordUsername").CurrentValue = userId;
                    }

                    if (entry.Property("UpdateUsername") != null)
                    {
                        entry.Property("UpdateUsername").CurrentValue = userId;
                    }

                    if (entry.Property("Id") != null)
                    {
                        entry.Property("Id").CurrentValue = Guid.NewGuid();
                    }

                    if (entry.Property("RecordDate") != null)
                    {
                        entry.Property("RecordDate").CurrentValue = today;
                    }

                    if (entry.Property("UpdateDate") != null)
                    {
                        entry.Property("UpdateDate").CurrentValue = today;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Property("UpdateDate") != null)
                    {
                        entry.Property("UpdateDate").CurrentValue = today;
                    }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}