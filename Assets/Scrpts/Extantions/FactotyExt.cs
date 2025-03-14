using UnityEngine;

public static class FactoryExt
{
    public static T Create<T>(this Factory<T> factory, int index, Transform parent)
        where T : MonoBehaviour, IObjectWithIndex
    {
        var obj = factory.Create(index);
        obj.transform.parent = parent;
        return obj;
    }
}
