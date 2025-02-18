using UnityEngine;

public static class Extensions
{
   public static float DistanceXZ(this Vector3 v1,  Vector3 v2)
    {
        return (float)(v1 - v2).magnitude;
    }
}
