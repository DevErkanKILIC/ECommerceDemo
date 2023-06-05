using ECommerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public DbSet<T> Table { get; }
    }
}
