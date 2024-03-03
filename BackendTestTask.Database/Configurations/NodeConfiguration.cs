using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTestTask.Database.Entities;

namespace BackendTestTask.Database.Configurations
{
    public class NodeConfiguration : EntityConfiguration<Node>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Node> builder)
        {
            builder.HasKey(n => n.Id);

            builder
                .Property(n => n.Name)
                .IsRequired();

            builder
                .HasOne(n => n.Tree)
                .WithMany(t => t.Nodes)
                .HasForeignKey(n => n.TreeId);

            builder
                .HasOne(n => n.ParentNode)
                .WithMany(n => n.ChildrenNodes)
                .HasForeignKey(n => n.ParentNodeId)
                .OnDelete(DeleteBehavior.NoAction);


            base.ConfigureEntity(builder);
        }
    }

}
