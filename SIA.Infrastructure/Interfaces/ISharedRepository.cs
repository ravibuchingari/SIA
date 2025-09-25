using SIA.Domain.Entities;

namespace SIA.Infrastructure.Interfaces
{
    public interface ISharedRepository
    {
        Task<List<LanguageVM>> GetLanguagesAsync();
        Task<List<TimeZoneVM>> GetTimeZonesAsync();
    }
}
