using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.exceptions.ExceptionsBase
{
    public class InvalidLoginException: UserException
    {
        public InvalidLoginException(): base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID) { }
    }
}
