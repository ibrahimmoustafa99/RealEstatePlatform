using RealEstatePlatform_API.Models;

namespace RealEstatePlatform_API.Repositories.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync();
        Task<Property?> GetPropertyByIdAsync(int id);

        Task<IEnumerable<Property>> GetPropertiesByAgentIdAsync(string AgentId);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(int id);


    }
}
