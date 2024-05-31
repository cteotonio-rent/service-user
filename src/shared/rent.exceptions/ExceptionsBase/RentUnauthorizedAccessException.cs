namespace rent.exceptions.ExceptionsBase
{
    public class RentUnauthorizedAccessException: UserException
    {
        public RentUnauthorizedAccessException() : base(ResourceMessagesException.UNAUTHORIZED_ACCESS) { }
    }
}
