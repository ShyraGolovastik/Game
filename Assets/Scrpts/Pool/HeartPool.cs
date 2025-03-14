using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    [SerializeField] private Pool<Heart> _pool;
    [SerializeField] private Transform MinX;
    [SerializeField] private Transform MaxX;
    [SerializeField] private GameHandler _handler;
    [SerializeField] private float _interval;

    private Coroutine _coroutine;

    public Pool<Heart> Pool => _pool;
    private void Start()
    {
        _handler.OnGameStart += () => _coroutine = StartCoroutine(Spawn());
        _handler.OnGameOver += () => StopCoroutine(_coroutine);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            var rnd = new Vector3(
                Random.Range(MinX.transform.position.x, MaxX.transform.position.x),
                0.0f,
               MinX.transform.position.z);

            _pool.Get(rnd);

            yield return new WaitForSeconds(_interval);
        }
    }
}
