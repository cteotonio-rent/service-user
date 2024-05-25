namespace rent.user.communication.Responses
{
    public class ResponseErrorJson
    {
        public IList<string> Errors { get; set; } = new List<string>();

        public ResponseErrorJson(IList<string> errors)  => Errors = errors;

        public ResponseErrorJson(string error) => Errors.Add(error);
    }
}
