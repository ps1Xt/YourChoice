using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain;

namespace YourChoice.Api.Database.configuration
{

   /* class PhotoPostConfiguration : IEntityTypeConfiguration<PhotoPost>
    {
        public void Configure(EntityTypeBuilder<PhotoPost> builder)
        {
            builder.HasMany(x => x.PostParts).WithOne(x => x.Post).OnDelete(DeleteBehavior.Cascade);
        }
    }*/
}
