using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreStarter.Infrastructure.Extensions.Auth;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreStarter.Application.Policies
{
    public class AuthServerPolicies : ICustomPolicyBuilder
    {
        public const string RolePermissionsPolicy = "RolePermissionsPolicy";

        public Dictionary<string, Action<AuthorizationPolicyBuilder>> GetPolicies() => new Dictionary<string, Action<AuthorizationPolicyBuilder>>
        {
            {RolePermissionsPolicy, policy => policy.RequireClaim("RolePermissionsChange", "allow")}
        };
    }
}
