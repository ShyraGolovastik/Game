using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private HeartSpawner _heartSpawner;
    [SerializeField] private SpawnEnemy _spawnEnemy;
    [SerializeField] private PlayerMover _player;
    [SerializeField] private Location _location;
    [SerializeField] private GameHandler _handler;


    public WorldData GetWorldData()
    {
        return new WorldData()
        {
            Hearts = GetDataFromPool(_heartSpawner.Pool).ToList(),
            Player = GetObjectData(_player.gameObject, _player.PrefabName),
            Location = GetObjectData(_location.gameObject, _location.PrefabName),
            Obstacles = GetDataFromPool(_spawnEnemy.Pool).ToList(),
            Score = _player.Scores,
            IsPaused = _handler.IsPaused,
            PlayerHealth = _player.Hp,
        };
    }

    public IEnumerable<(ObjectData, int)> GetDataFromPool<T>(Pool<T> pool)
        where T : Prefab, IObjectFromPool<T>, IObjectWithIndex
    {
        foreach (var item in pool.InstancesLists.Values)
        {
            foreach(var obj in item)
            {
                yield return (GetObjectData(obj.gameObject, obj.PrefabName), obj.Index);
            }
        }
    }

    private ObjectData GetObjectData(GameObject obj, string prefabName)
    {
        return new ObjectData()
        {
            IsActive = obj.tag != "InPool",
            Position = obj.transform.position,
            Rotation = obj.transform.localEulerAngles,
            Scale = obj.transform.localScale,
            PrefabName = prefabName,
            RbData = GetRbData(obj.gameObject)
        };
    }

    private RbData GetRbData(GameObject obj)
    {
        RbData rbData = null;
        if (obj.TryGetComponent(out Rigidbody rb))
        {
            rbData = new RbData()
            {
                LineVelocity = rb.linearVelocity,
                AngularVelocity = rb.angularVelocity,
                IsKinematic = rb.isKinematic,
            };
        }
        return rbData;
    }

    public void LoadData(WorldData worldData)
    {
        DestroyAllPrefabs();
        var player = AddAllStats(
            Instantiate(
                Resources.Load<GameObject>(worldData.Player.PrefabName)),
                worldData.Player)
            .GetComponent<PlayerMover>();
        player.Hp = worldData.PlayerHealth;
        player.Scores = worldData.Score;
        player.OnHealthChanged = _player.OnHealthChanged;
        _player = player;
        _handler.Player = player;

        var location = AddAllStats(Instantiate(
            Resources.Load<GameObject>(worldData.Location.PrefabName)),
            worldData.Location)
            .GetComponent<Location>();
        _location = location;

        foreach (var heartData in worldData.Hearts)
        {
            var heart = AddAllStats(Instantiate(
                    Resources.Load<GameObject>(heartData.obj.PrefabName)),
                    heartData.obj)
                .GetComponent<Heart>();
            if (!_spawnEnemy.Pool.InstancesLists.ContainsKey(heartData.index))
                _heartSpawner.Pool.InstancesLists[heartData.index] = new List<Heart>();
            _heartSpawner.Pool.InstancesLists[heartData.index].Add(heart);
            heart.ParentPool =  _heartSpawner.Pool;
        }
        foreach (var obstacleData in worldData.Obstacles)
        {
            var enemy = AddAllStats(Instantiate(
                    Resources.Load<GameObject>(obstacleData.obj.PrefabName)),
                    obstacleData.obj)
                .GetComponent<Obstacle>();
            if(!_spawnEnemy.Pool.InstancesLists.ContainsKey(obstacleData.index))
                _spawnEnemy.Pool.InstancesLists[obstacleData.index] = new List<Obstacle>();
            _spawnEnemy.Pool.InstancesLists[obstacleData.index].Add(enemy);
            enemy.ParentPool = _spawnEnemy.Pool;
        }

        _handler.IsPaused = worldData.IsPaused;
        worldData = new WorldData()
        {
            Hearts = GetDataFromPool(_heartSpawner.Pool).ToList(),
            Player = GetObjectData(player.gameObject, player.PrefabName),
            Location = GetObjectData(_location.gameObject, _location.PrefabName),
            Obstacles = GetDataFromPool(_spawnEnemy.Pool).ToList(),
            Score = _player.Scores,
            IsPaused = _handler.IsPaused,
            PlayerHealth = _player.Hp,
        };
    }

    private GameObject AddAllStats(GameObject obj, ObjectData data)
    {
        obj.transform.position = data.Position;
        obj.transform.localEulerAngles = data.Rotation;
        obj.transform.localScale = data.Scale;
        obj.SetActive(data.IsActive);
        if (obj.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = data.RbData.LineVelocity;
            rb.angularVelocity = data.RbData.AngularVelocity;
            rb.isKinematic = data.RbData.IsKinematic;
        } 

        return obj;
    }

    public void DestroyAllPrefabs()
    {
        Destroy(_player.gameObject);
        Destroy(_location.gameObject);
        foreach(var list in _spawnEnemy.Pool.InstancesLists.Values)
        {
            foreach (var item in list)
            {
                Destroy(item.gameObject);
            }
        }
        foreach(var list in _spawnEnemy.Pool.InstancesLists.Values)
            list.Clear();

        foreach(var list in _heartSpawner.Pool.InstancesLists.Values)
        {
            foreach (var item in list)
            {
                Destroy(item.gameObject);
            }
        }
        foreach (var list in _heartSpawner.Pool.InstancesLists.Values)
            list.Clear();
    }
}
