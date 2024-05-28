using Bogus;
using rent.communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestUtilities.Requests
{
    public class RequestLoginJsonBuilder
    {
        public static RequestLoginJson Build(int passwordLength = 10)
        {
            return new Faker<RequestLoginJson>()
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email())
                .RuleFor(user => user.Password, f => f.Internet.Password(passwordLength))
                .Generate();
        }
    }
}
