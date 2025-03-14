using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class Pool<T>: IPool<T>
    where T : MonoBehaviour, IObjectFromPool<T>, IObjectWithIndex
{
    [SerializeField] private Factory<T> _factory;
    [SerializeField] private Transform _parent;

    private Dictionary<int, List<T>> _instancesLists;
    public Dictionary<int, List<T>> InstancesLists => _instancesLists;
    private string _tag;
    public Pool()
    {
        
        _instancesLists = new();
    }

    public virtual T Create(int index)
    {
        var obj = _factory.Create(index, _parent);
        obj.ParentPool = this;

        return obj;
    }

    public T Get()
    {
        T obj = default(T);
        var i = Random.Range(0, _factory.InstancesCount);
        if(!_instancesLists.ContainsKey(i) || _instancesLists[i] == null)
            _instancesLists[i] = new List<T>();
        try
        {
         //   _instancesLists[i] = _instancesLists[i].Where(x => x != null).ToList();
            var freeObj = _instancesLists[i].Where(x => x.tag == "InPool").FirstOrDefault();
            if (freeObj == default(T))
            {
                obj = Create(i);
                _instancesLists[i].Add(obj);
            }
            else
                obj = freeObj;

            obj.gameObject.SetActive(true);
            obj.gameObject.tag = obj.Tag;
            return obj;
        }
        catch
        {
            Debug.Log(i);
        }
        return null;
    }
    public T Get(Vector3 pos)
    {
        var obj = Get();
        obj.transform.position = pos;

        return obj;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.gameObject.tag = "InPool";
        //if(!_instancesQueues[obj.Index].Contains(obj))
        //    _instancesQueues[obj.Index].Add(obj);
    }
}
