using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.exceptions.ExceptionsBase
{
    public class InvalidFileTypeException: UserException
    {
        public InvalidFileTypeException() : base(ResourceMessagesException.FILE_TYPE_INVALID) { }
    }
}
