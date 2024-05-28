using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.infrastructure.Security.Access
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string signingKey) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

    }
}
