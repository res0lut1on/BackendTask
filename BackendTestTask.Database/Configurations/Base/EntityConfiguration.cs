using BackendTestTask.Database.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using BackendTestTask.Services.Services.Generic.Interfaces;

public class EntityConfiguration<TEntity> : EntityConfiguration<TEntity, int>
    where TEntity : class, IEntity
{ }

public class EntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TKey>
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigurePrimaryKey(builder);

        ConfigureEntity(builder);
    }

    protected virtual void ConfigureEntity(EntityTypeBuilder<TEntity> builder)
    { }

    private void ConfigurePrimaryKey(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}
