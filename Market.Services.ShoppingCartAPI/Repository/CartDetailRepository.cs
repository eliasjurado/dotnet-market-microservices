using Market.Domain.Models;
using Market.Services.CartAPI.Data;
using Market.Services.CartAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Services.CartAPI.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public CartDetailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(CartDetail detail)
        {
            await Task.Run(() => _db.CartDetails.Add(detail));
        }

        public async Task<ICollection<CartDetail>> GetAsync(Expression<Func<CartDetail, bool>> expression)
        {
            return await _db.CartDetails.Where(expression).ToListAsync();
        }

        public async Task<ICollection<CartDetail>> GetAsync()
        {
            return await _db.CartDetails.ToListAsync();
        }

        public async Task<CartDetail> GetAsync(long headerId, long detailId)
        {
            return await _db.CartDetails.FirstOrDefaultAsync(x => x.CartHeaderId == headerId && x.CartDetailId == detailId);
        }

        public async Task RemoveAsync(long headerId, long detailId)
        {
            await Task.Run(async () => _db.Remove(await GetAsync(headerId, detailId)));
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartDetail detail)
        {
            await Task.Run(() => _db.CartDetails.Update(detail));
        }
    }
}
