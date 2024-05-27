﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.user.domain.Entities
{
    public class User: EntityBase
    {
        public string Name { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid UserUniqueIdentifier { get; set; } = Guid.Empty;

    }
}
