using Bogus;
using rent.user.communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestUtilities.Requests
{
    public class RequestUpdateUserJsonBuilder
    {
        public static RequestUpdateUserJson Build()
        {
            return new Faker<RequestUpdateUserJson>()
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Email, (f, user) => f.Internet.Email(user.Name));
        }
    }
}
