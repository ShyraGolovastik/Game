using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factory<T>
    where T : MonoBehaviour, IObjectWithIndex
{
    [SerializeField] private List<T> _instances = new List<T>();

    public int InstancesCount => _instances.Count;
    public T Create(int index)
    {
        var obj = MonoBehaviour.Instantiate(_instances[index]);
        obj.Index = index;
        return obj;
    }
    public T Create(Vector3 position, int index)
    {
        var obj = Create(index);
        obj.transform.position = position;

        return obj;
    }
}
