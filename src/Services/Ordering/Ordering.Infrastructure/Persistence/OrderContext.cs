using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        //public ApplicationDbContext(
        //DbContextOptions<ApplicationDbContext> options,
        //IOptions<OperationalStoreOptions> operationalStoreOptions,
        //ICurrentUserService currentUserService,
        //IDomainEventService domainEventService,
        //IDateTime dateTime) : base(options, operationalStoreOptions)
        //{
        //    _currentUserService = currentUserService;
        //    _domainEventService = domainEventService;
        //    _dateTime = dateTime;
        //}


        public DbSet<Order> Orders { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

    }
}
