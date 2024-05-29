using rent.domain.Enuns;

namespace rent.domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid UserUniqueIdentifier { get; set; } = Guid.Empty;

        public UserType? UserType { get; set; } = null;

        public string? NRLE { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; } = DateTime.MinValue;
        public string? DriversLicense { get; set; } = string.Empty;
        public string? DriversLicenseCategory { get; set; } = string.Empty;
        public string? DriversLicenseImage { get; set; } = string.Empty;
    }
}
