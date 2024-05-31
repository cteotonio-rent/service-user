namespace rent.exceptions.ExceptionsBase
{
    public class NotFoundException: UserException
    {
        public NotFoundException() : base(ResourceMessagesException.RESOURCE_NOT_FOUND) { }
    }
}
