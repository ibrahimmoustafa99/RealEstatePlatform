using Microsoft.EntityFrameworkCore;
using RealEstatePlatform_API.Data;
using RealEstatePlatform_API.Models;
using RealEstatePlatform_API.Repositories.Interfaces;

namespace RealEstatePlatform_API.Repositories.Implementations
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync()
        {
            return await _context.Properties
                         .Include(p => p.Images)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByAgentIdAsync(string AgentId)
        {
            return await _context.Properties
                         .Where(p => p.AgentId == AgentId)
                         .Include(p => p.Images)
                         .ToListAsync();
        }
        public async Task<Property?> GetPropertyByIdAsync(int Id)
        {
            return await _context.Properties.
                         Include(p => p.Images).
                         FirstOrDefaultAsync(p=> p.Id==Id);
        }

        public async Task AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
            
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var property = _context.Properties.Find(id);
            if (property != null) {
                property.IsAvailable = false; // Soft delete
                await _context.SaveChangesAsync();

            }
        }

        
    }
}
