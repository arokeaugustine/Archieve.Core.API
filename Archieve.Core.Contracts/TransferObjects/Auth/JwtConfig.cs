using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Auth
{
    public class JwtConfig
    {
        public string Secret { get; set; } = string.Empty;
        public int RefreshTokenTTl { get; set; }
        public int TokenValidityPeriod { get; set; }
    }
}
