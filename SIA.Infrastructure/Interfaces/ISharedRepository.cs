using SIA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Infrastructure.Interfaces
{
    public interface ISharedRepository
    {
        Task<List<LanguageVM>> GetLanguagesAsync();
        Task<List<TimeZoneVM>> GetTimeZonesAsync();
    }
}
