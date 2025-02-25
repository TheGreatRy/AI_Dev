using UnityEngine;

public static class Extensions
{
   public static float DistanceXZ(this Vector3 v1,  Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow(v2.x - v1.x, 2) + Mathf.Pow(v2.z - v1.z, 2));
    }
}
