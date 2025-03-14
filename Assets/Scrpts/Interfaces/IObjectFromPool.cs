using UnityEngine;

public interface IObjectFromPool<T>
{
    public IPool<T> ParentPool { get; set; }
    public string Tag { get; }

    public void ReturnToPool(); 
}
