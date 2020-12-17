using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain;
namespace YourChoice.Api.Database.configuration
{
    class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Text).HasMaxLength(500);
            builder.HasOne(x => x.User).WithMany(x => x.Messages).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
