using UnityEngine;

public abstract class Prefab: MonoBehaviour
{
    [SerializeField] protected string _prefabName;
   public string PrefabName => _prefabName;
}
