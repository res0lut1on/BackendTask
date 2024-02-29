using BackendTestTask.Database.Interfaces;
using BackendTestTask.UserContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;

namespace BackendTestTask.Database
{
    public class BaseContext : DbContext, IBaseContext
    {
        private readonly IUserContext _userContext;

        public BaseContext(DbContextOptions options, IUserContext userContext) : base(options)
        {
            _userContext = userContext;
        }

        public BaseContext(DbContextOptions options) : base(options) { }

        protected virtual int? UserId
        {
            get => _userContext.Id.HasValue ? _userContext.Id.Value : null;
        }

        protected void SetCreator(InternalEntityEntry entry)
        {
            if (entry.Entity is ICreated created && UserId.HasValue)
            {
                if (created.CreatedBy == 0)
                {
                    created.CreatedBy = UserId.Value;
                }
                created.CreatedOn = DateTime.UtcNow;
            }
        }

        protected void SetModifier(InternalEntityEntry entry)
        {
            if (entry.Entity is IModified modified && UserId.HasValue)
            {
                modified.ModifiedBy = UserId.Value;
                modified.ModifiedOn = DateTime.UtcNow;
            }
        }

        protected void SetSoftDelete(InternalEntityEntry entry)
        {
            if (entry.Entity is IDeleted deleted)
            {
                entry.SetEntityState(EntityState.Modified);
                deleted.IsDeleted = true;
                SetModifier(entry);
            }
        }

        protected virtual void OnAdded(InternalEntityEntry entry)
        {
            SetCreator(entry);
        }

        protected virtual void OnModified(InternalEntityEntry entry)
        {
            SetModifier(entry);
        }

        protected virtual void OnDeleted(InternalEntityEntry entry)
        {
            SetSoftDelete(entry);
        }

        protected virtual async void BeforeSave()
        {
            ChangeTracker.DetectChanges();

            var publishedEntries = (this as IDbContextDependencies).StateManager.GetEntriesForState(true, true, true)
                .ToList();

            if (publishedEntries.Any())
            {
                foreach (var entry in publishedEntries)
                {
                    if (entry.EntityState == EntityState.Added)
                    {
                        OnAdded(entry);
                    }

                    if (entry.EntityState is EntityState.Modified or EntityState.Added)
                    {
                        OnModified(entry);
                    }

                    if (entry.EntityState == EntityState.Deleted)
                    {
                        OnDeleted(entry);
                    }
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
