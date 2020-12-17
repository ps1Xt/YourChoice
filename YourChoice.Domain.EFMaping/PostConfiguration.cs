using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain;

namespace YourChoice.Domain.EFMaping
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.HasOne(x => x.User).WithMany(x => x.Posts).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.PostParts).WithOne(x => x.Post).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
