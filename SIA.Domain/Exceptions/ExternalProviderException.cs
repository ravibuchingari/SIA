using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Exceptions
{
    public class ExternalProviderException(string provider, string message) : Exception($"External provider: {provider}, Error occurred: {message}");
}
