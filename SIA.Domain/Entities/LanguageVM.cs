using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Entities
{
    public class LanguageVM
    {
        public string LanguageCode { get; set; } = null!;

        public string LanguageName { get; set; } = null!;
    }
}
