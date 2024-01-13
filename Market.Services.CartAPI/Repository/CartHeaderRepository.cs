using Market.Domain.Models;
using Market.Services.CartAPI.Data;
using Market.Services.CartAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Services.CartAPI.Repository
{
    public class CartHeaderRepository : ICartHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public CartHeaderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(CartHeader header)
        {
            await Task.Run(() => _db.CartHeaders.Add(header));
        }

        public async Task<ICollection<CartHeader>> GetAsync(Expression<Func<CartHeader, bool>> expression)
        {
            return await _db.CartHeaders.Where(expression).ToListAsync();
        }

        public async Task<ICollection<CartHeader>> GetAsync()
        {
            return await _db.CartHeaders.ToListAsync();
        }

        public async Task<CartHeader> GetAsync(Guid userId)
        {
            return await _db.CartHeaders.FirstOrDefaultAsync(x => x.CreatedBy.Equals(userId));
        }

        public async Task<CartHeader> GetAsync(Guid userId, long headerId)
        {
            return await _db.CartHeaders.FirstOrDefaultAsync(x => x.CreatedBy.Equals(userId) && x.CartHeaderId == headerId);
        }

        public async Task<CartHeader> GetAsync(long headerId)
        {
            return await _db.CartHeaders.FirstOrDefaultAsync(x => x.CartHeaderId == headerId);
        }

        public async Task RemoveAsync(long headerId)
        {
            await Task.Run(async () => _db.Remove(await GetAsync(headerId)));
        }

        public async Task RemoveAsync(Guid userId, long headerId)
        {
            await Task.Run(async () => _db.Remove(await GetAsync(userId, headerId)));
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartHeader header)
        {
            await Task.Run(() => _db.CartHeaders.Update(header));
        }
    }
}
