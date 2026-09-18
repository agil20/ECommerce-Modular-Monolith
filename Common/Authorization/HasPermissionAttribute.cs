using Microsoft.AspNetCore.Authorization;

namespace Common.Authorization;

// The permission name doubles as the policy name; policies are registered per permission in Program.cs.
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(permission)
    {
    }
}
