using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskEntity = DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Task;

namespace DeveloperManagement.TeamManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime,
            DbContextOptions options) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _domainEventService = domainEventService;
        }

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        
        public DbSet<RelatedWork> RelatedWorks { get; set; }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("created_by").CurrentValue = _currentUserService.UserId;
                        entry.Property("created").CurrentValue = _dateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Property("last_modified_by").CurrentValue = _currentUserService.UserId;
                        entry.Property("last_modified").CurrentValue = _dateTime.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(cancellationToken);

            return result;
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(CancellationToken cancellationToken)
        {
            var domainEventEntities = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .ToArray();

            foreach (var domainEvent in domainEventEntities)
            {
                await _domainEventService.Publish(domainEvent);
            }
        }
    }
}