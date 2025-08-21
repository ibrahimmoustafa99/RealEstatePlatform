using RealEstatePlatform_API.DTOs.Property;
using RealEstatePlatform_API.Models;

namespace RealEstatePlatform_API.Repositories.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync();
        Task<Property?> GetPropertyByIdAsync(int id);

        Task<IEnumerable<Property>> GetPropertiesByAgentIdAsync(string AgentId);
        Task AddAsync(Property property);
        Task UpdateAsync(int id,PropertyUpdateDTO property);
        Task DeleteAsync(int id);


    }
}
