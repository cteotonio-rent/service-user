using Bogus;
using Bogus.Extensions.Brazil;
using CommomTestUtilities.Cryptography;
using rent.domain.Entities;

namespace CommomTestUtilities.Entities
{
    public class UserBuilder
    {
        
        public static (User user, string password) Build(int passwordLength = 10)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var password = new Faker().Internet.Password(passwordLength);

            var user = new Faker<User>()
                .RuleFor(user => user._id, () => new MongoDB.Bson.ObjectId() )
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email())
                .RuleFor(user => user.Password, f => passwordEncripter.Encrypt(password))
                .RuleFor(user => user.UserUniqueIdentifier, _ => Guid.NewGuid())
                .RuleFor(user => user.UserType, _ => rent.domain.Enuns.UserType.DeliveryPerson)
                .RuleFor(user => user.Active, _ => true)
                .RuleFor(user => user.CreateOn, _ => DateTime.Now)
                .RuleFor(user => user.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(user => user.NRLE, f => f.Company.Cnpj())
                .RuleFor(user => user.DriversLicense, f => f.Random.Number(8).ToString())
                .RuleFor(user => user.DriversLicenseCategory, _ => "A")
                .Generate();

            return (user, password);
        }
    }
}
