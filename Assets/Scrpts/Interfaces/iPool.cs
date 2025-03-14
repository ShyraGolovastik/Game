using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public interface IPool<T>
{
    public T Create(int index);

    public T Get();    
    public T Get(Vector3 pos);
    public void Release(T obj);
}
