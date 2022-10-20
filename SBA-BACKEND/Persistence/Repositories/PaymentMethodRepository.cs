using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Domain.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Persistence.Repositories
{
    public class PaymentMethodRepository : BaseRepository, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PaymentMethod>> ListAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }
    }
}
