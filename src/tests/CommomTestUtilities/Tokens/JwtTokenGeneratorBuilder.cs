using rent.user.infrastructure.Security.Access.Generator;

namespace CommomTestUtilities.Token
{
    public class JwtTokenGeneratorBuilder
    {
        public static JwtTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, singingKey: "tttttttttttttttttttttttttttttttt");
        
    }
}
