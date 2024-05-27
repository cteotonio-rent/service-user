using Bogus;
using CommomTestUtilities.Cryptography;
using rent.user.communication.Requests;
using rent.user.domain.Entities;

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
                .Generate();

            return (user, password);
        }
    }
}
