using rent.user.infrastructure.Security.Access.Generator;

namespace UseCases.Test
{
    public class JwtTokenGeneratorBuilder
    {
        public static JwtTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, singingKey: "tttttttttttttttttttttttttttttttt");
        
    }
}
