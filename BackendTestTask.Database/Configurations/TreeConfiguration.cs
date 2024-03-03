using BackendTestTask.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Database.Configurations
{
    public class TreeConfiguration : EntityConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .Property(t => t.Name)
                .IsRequired();

            builder
                .HasMany(t => t.Nodes)
                .WithOne(n => n.Tree)
                .HasForeignKey(n => n.TreeId)
                .OnDelete(DeleteBehavior.Cascade);  // CASCADE REMOVE CHILDREN NODES

            base.ConfigureEntity(builder);
        }
    }

}
