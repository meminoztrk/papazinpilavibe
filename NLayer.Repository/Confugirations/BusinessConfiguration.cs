using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Confugirations
{
    internal class BusinessConfiguration: IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            //builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.UserId);
        }
    }
}
