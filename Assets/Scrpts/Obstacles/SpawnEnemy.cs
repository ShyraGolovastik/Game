using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform _mimX;
    [SerializeField] private Transform _maxX;
    [SerializeField] private GameHandler _handler;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private float _offset;
    [SerializeField] private float _spawnInterval;

    private Coroutine _coroutine;

    public EnemyPool Pool => _enemyPool;
    private void Start()
    {
        _handler.OnGameStart += () => _coroutine = StartCoroutine(Spawn());
        _handler.OnGameOver += () => StopCoroutine(_coroutine);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            var RandomX = Random.Range(_mimX.transform.position.x,
               _maxX.transform.position.x);
            Vector3 offset = new Vector3(RandomX, 0.0f, _mimX.transform.position.z);
            _enemyPool.Get(offset);
            yield return new WaitForSeconds(_spawnInterval);
            _spawnInterval /= 1.001f;
        }
    }
}
