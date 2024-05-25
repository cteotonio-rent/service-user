namespace rent.user.exceptions.ExceptionsBase
{
    public class ErrorOnValidationException: UserException
    {
        public IList<string> ErrorMessages { get; set; } = new List<string>();

        public ErrorOnValidationException(IList<string> erros) {
            ErrorMessages = erros;
        }
    }
}
