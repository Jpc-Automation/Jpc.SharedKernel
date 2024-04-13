using System.Security.Claims;

namespace Jpc.SharedKernel;

public interface IIdentityServiceBase
{
    string GetUserIdentity();

    string GetUserName();

    Guid GetUserId();

    string GetUserEmail();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim> GetUserClaims();
}
