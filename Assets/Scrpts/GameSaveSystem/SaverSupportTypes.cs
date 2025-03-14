using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Barrel,
    Box,
    Stone
}

[System.Serializable]
public class ObjectData
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public string PrefabName;
    public RbData RbData;
    public bool IsActive;
}
[System.Serializable]
public class RbData
{
    public Vector3 LineVelocity;
    public Vector3 AngularVelocity;
    public bool IsKinematic;
}
[System.Serializable]
public class WorldData
{
    public float Score;
    public float PlayerHealth;
    public bool IsPaused;

    public ObjectData Player;
    public ObjectData Location;
    public List<(ObjectData obj, int index)> Obstacles;
    public List<(ObjectData obj, int index)> Hearts;
}