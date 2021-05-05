using System.Linq;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Seed
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            if (!context.Activities.Any())
            {
                context.Activities.Add(Activity.Deployment);
                context.Activities.Add(Activity.Design);
                context.Activities.Add(Activity.Development);
                context.Activities.Add(Activity.Documentation);
                context.Activities.Add(Activity.Requirements);
                context.Activities.Add(Activity.Testing);

                await context.SaveChangesAsync();
            }
        }
    }
}