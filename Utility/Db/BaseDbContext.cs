using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility.Db.Interface;

namespace Utility.Db
{
    public class BaseDbContext : DbContext
    {

        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }

        private void OnSaveChanges()
        {
            var concurrentEntries = base.ChangeTracker.Entries().Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in concurrentEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    (entry.Entity as IBaseModel).OnCreate();
                }
                else if (entry.State == EntityState.Modified)
                {
                    (entry.Entity as IBaseModel).OnUpdate();
                }
            }
        }

        public override int SaveChanges()
        {
            this.OnSaveChanges();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.OnSaveChanges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.OnSaveChanges();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.OnSaveChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

    }


}
