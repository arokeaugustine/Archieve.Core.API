﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Archieve.Domain.Helpers.Authorizations
{
    public class PermissionRequirement : IAuthorizationRequirement
    {

        public string Permission {  get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
