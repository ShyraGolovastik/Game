using UnityEngine;

public static class VectorExtantoins
{
    public static Vector3 IgnoreHeight(this Vector3 v) => new Vector3(v.x, 0.0f, v.z);
}
