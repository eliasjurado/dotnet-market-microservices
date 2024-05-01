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

        public async Task<ICollection<CartDetail>> GetAsync(Guid userId)
        {
            return await _db.CartDetails.Where(x => x.CreatedBy.Equals(userId)).ToListAsync();
        }

        public async Task<ICollection<CartDetail>> GetAsync(Guid userId, long headerId)
        {
            return await _db.CartDetails.Where(x => x.CreatedBy.Equals(userId) && x.CartHeaderId == headerId).ToListAsync();
        }

        public async Task<ICollection<CartDetail>> GetAsync(long headerId)
        {
            return await _db.CartDetails.Where(x => x.CartHeaderId == headerId).ToListAsync();
        }

        public async Task<CartDetail> GetAsync(Guid userId, long headerId, long detailId)
        {
            return await _db.CartDetails.FirstOrDefaultAsync(x => x.CreatedBy.Equals(userId) && x.CartHeaderId == headerId && x.CartDetailId == detailId);
        }

        public async Task RemoveAsync(Guid userId, long headerId)
        {
            var collection = await GetAsync(userId, headerId);
            foreach (var item in collection)
            {
                await Task.Run(() => Task.FromResult(_db.Remove(item)));
            }
        }

        public async Task RemoveAsync(long headerId, long detailId)
        {
            await Task.Run(async () => _db.Remove(await GetAsync(headerId, detailId)));
        }

        public async Task RemoveAsync(Guid userId, long headerId, long detailId)
        {
            await Task.Run(async () => _db.Remove(await GetAsync(userId, headerId, detailId)));
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
