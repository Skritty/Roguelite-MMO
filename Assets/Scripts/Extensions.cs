using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T IfNull<T>(this T o, T returnIfNull)
    {
        if (o == null) return returnIfNull;
        else return o;
    }
}
