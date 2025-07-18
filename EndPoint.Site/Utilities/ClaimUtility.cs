using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EndPoint.Site.Utilities;

public static class ClaimUtility
{
    public static long? GetUserId(ClaimsPrincipal user)
    {
        //try
        //{
        var claimsIdentity = user.Identity as ClaimsIdentity;
        if (claimsIdentity != null)
        {
            var usId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usId != null)
            {
                var userId = long.Parse(usId);
                return userId;
            }
        }

        return null;
        //}
        //catch (Exception e)
        //{
        //    return null;
        //}
    }

    public static string GetUserEmail(ClaimsPrincipal User)
    {
        try
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return claimsIdentity.FindFirst(ClaimTypes.Email).Value;
        }
        catch (Exception)
        {
            return null;
        }
    }


    public static List<string> GetRoles(ClaimsPrincipal User)
    {
        try
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var rolse = new List<string>();
            foreach (var item in claimsIdentity.Claims.Where(p => p.Type.EndsWith("role"))) rolse.Add(item.Value);
            return rolse;
        }
        catch (Exception)
        {
            return null;
        }
    }
}