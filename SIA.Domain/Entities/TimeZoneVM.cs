using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Entities
{
    public class TimeZoneVM
    {
        public string TimeZoneName { get; set; }

        public string Utcoffset { get; set; }

        public string CommonRegions { get; set; }
    }
}
