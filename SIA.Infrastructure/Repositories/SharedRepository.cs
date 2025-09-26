using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIA.Domain.Entities;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.Interfaces;

namespace SIA.Infrastructure.Repositories
{
    public class SharedRepository(AppDBContext dbContext, IMapper mapper) : BaseRepository(dbContext ?? null), ISharedRepository
    {
        public async Task<AuthConfigVM?> GetAuthConfigAsync(string provider)
        {
            var authConfig = await dbContext.AuthConfigs.Where(col => col.AuthProvider == provider && col.IsActive == true).FirstOrDefaultAsync();
            if (authConfig != null)
                return mapper.Map<AuthConfigVM>(authConfig);
            else
                return null;
        }

        public async Task<List<LanguageVM>> GetLanguagesAsync()
        {
            return await dbContext.Languages.Select(row => new LanguageVM()
            {
                LanguageCode = row.LanguageCode,
                LanguageName = row.LanguageName
            }).ToListAsync();
        }

        public async Task<List<TimeZoneVM>> GetTimeZonesAsync()
        {
            return await dbContext.SiatimeZones.Select(row => new TimeZoneVM()
            {
                TimeZoneName = row.TimeZoneName,
                CommonRegions = $"{row.TimeZoneName}-{row.CommonRegions}"
            }).ToListAsync();
        }
    }
}
