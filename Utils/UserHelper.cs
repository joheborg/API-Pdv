using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace API_Pdv.Utils;

public static class UserHelper
{
    public static int? GetCurrentUserEmpresaId(HttpContext httpContext)
    {
        var empresaIdClaim = httpContext.User.FindFirst("EmpresaId");
        if (empresaIdClaim != null && int.TryParse(empresaIdClaim.Value, out int empresaId))
        {
            return empresaId;
        }
        return null;
    }

    public static int GetCurrentUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        throw new InvalidOperationException("Usuário não autenticado");
    }

    public static string GetCurrentUserEmail(HttpContext httpContext)
    {
        var emailClaim = httpContext.User.FindFirst(ClaimTypes.Email);
        return emailClaim?.Value ?? string.Empty;
    }

    public static string GetCurrentUserName(HttpContext httpContext)
    {
        var nameClaim = httpContext.User.FindFirst(ClaimTypes.Name);
        return nameClaim?.Value ?? string.Empty;
    }

    public static string GetCurrentUserProfile(HttpContext httpContext)
    {
        var profileClaim = httpContext.User.FindFirst("Perfil");
        return profileClaim?.Value ?? string.Empty;
    }
} 