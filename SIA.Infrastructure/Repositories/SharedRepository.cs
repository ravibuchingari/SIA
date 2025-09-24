using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIA.Domain.Entities;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Infrastructure.Repositories
{
    public class SharedRepository(AppDBContext dbContext, IMapper mapper) : BaseRepository(dbContext ?? null), ISharedRepository
    {
        public async Task<List<LanguageVM>> GetLanguagesAsync()
        {
            return await dbContext.Languages.Select(row => new LanguageVM()
            {
                LanguageCode = row.LanguageCode,
                LanguageType = row.LanguageType
            }).OrderBy(l => l.LanguageCode).ToListAsync();
        }

        public async Task<List<TimeZoneVM>> GetTimeZonesAsync()
        {
            return await dbContext.SiatimeZones.Select(row => new TimeZoneVM()
            {
                TimeZoneName = row.TimeZoneName,
                CommonRegions = $"{row.TimeZoneName}-{row.CommonRegions}"
            }).OrderBy(t => t.TimeZoneName).ToListAsync();
        }
    }
}
