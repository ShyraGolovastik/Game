using UnityEngine;

public class Heart : Prefab, IObjectFromPool<Heart>, IObjectWithIndex
{
    public IPool<Heart> ParentPool { get; set; }
    public int Index { get; set; }
    public string Tag { get; } = "Heal";
    public void ReturnToPool()
    {
        ParentPool.Release(this);
    }
}
