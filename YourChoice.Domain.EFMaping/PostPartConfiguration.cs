using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain;

namespace YourChoice.Domain.EFMaping
{
    public class PostPartConfiguration : IEntityTypeConfiguration<PostPart>
    {
        public void Configure(EntityTypeBuilder<PostPart> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(150);
        }
    }
}
