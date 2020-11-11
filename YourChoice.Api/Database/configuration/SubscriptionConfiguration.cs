using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain;


namespace YourChoice.Api.Database.configuration
{
    class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => new { x.ToWhomId, x.WhoId });

            builder.HasOne(x => x.ToWhom)
                   .WithMany(x => x.Subscribers)
                   .HasForeignKey(x => x.ToWhomId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Who)
                   .WithMany(x => x.Subscriptions)
                   .HasForeignKey(x => x.WhoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
