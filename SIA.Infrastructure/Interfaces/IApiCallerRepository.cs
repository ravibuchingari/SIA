using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Infrastructure.Interfaces
{
    public interface IApiCallerRepository
    {
        Task<T?> GetAsync<T>(string url, string? bearerToken = null);
        Task<T?> PostAsync<T>(string url, object payload, string? bearerToken = null);
    }
}
