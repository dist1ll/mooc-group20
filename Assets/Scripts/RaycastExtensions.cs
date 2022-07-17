using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastExtensions
{
    public static bool IsDefault(this RaycastHit r)
    {
        return r.Equals(default(RaycastHit));
    }
}
