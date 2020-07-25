﻿using Hcqn.Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hcqn.Infra.CrossCutting.Identity.Interfaces
{
    public interface IRoleManager
    {
        Task<IdentityResult> CreateRole(Role applicationRole);
        Task<IdentityResult> AddClaim(Role applicationRole, Claim claim);
        Task<Role> GetById(Guid id);
        Task<IdentityResult> Update(Role applicationRole);
        Task ClearAllClaims(Role applicationRole);
    }
}
