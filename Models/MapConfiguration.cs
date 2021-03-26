using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class MapConfiguration
    {
        public string KeyVaultName { get; set; }
        public string SecretName { get; set; }
        public string MapBoxToken { get; set; }
    }
}
