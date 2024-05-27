namespace rent.user.domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generate(Guid userIdentifier);
    }
}
