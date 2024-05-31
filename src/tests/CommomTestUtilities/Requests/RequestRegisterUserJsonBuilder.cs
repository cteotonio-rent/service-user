using Bogus;
using Bogus.Extensions.Brazil;
using rent.communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, f => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, f => f.Internet.Password(passwordLength))
                .RuleFor(user => user.NRLE, f => f.Company.Cnpj())
                .RuleFor(user => user.DateOfBirth, f => f.Date.Past(18, System.DateTime.Now.AddYears(-18)))
                .RuleFor(user => user.DriversLicense, f => f.Random.Replace("###########"))
                .RuleFor(user => user.DriversLicenseCategory, f => "A")
                .Generate();
        } 
    }
}
