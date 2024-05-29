namespace rent.communication.Requests
{
    public class RequestRegisterUserJson
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NRLE { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string DriversLicense { get; set; } = string.Empty;
        public string DriversLicenseCategory { get; set; } = string.Empty;
    }
}
