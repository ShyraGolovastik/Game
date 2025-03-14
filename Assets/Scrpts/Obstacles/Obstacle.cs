using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Obstacle : Prefab, IObjectWithIndex, IObjectFromPool<Obstacle>
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxZCords;
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] private ObstacleType _type;

    private Rigidbody _rigidbody;

    public int Index { get; set; }
    public IPool<Obstacle> ParentPool { get; set; }
    public string Tag { get; } = "Enemy";
    public ObstacleType Type => _type;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ReturnToPool()
    {
        ParentPool.Release(this);
    }

    void IObjectFromPool<Obstacle>.ReturnToPool()
    {
        ReturnToPool();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);
        }
    }
    private void Update()
    {
        _rigidbody.AddForce(Vector3.back * _acceleration * Time.deltaTime, ForceMode.VelocityChange);
        if (transform.position.z <= _maxZCords)
            ReturnToPool();
    }

    private void OnEnable()
    {
       
        _rigidbody.AddForce(Vector3.back * _startSpeed, ForceMode.VelocityChange);
    }
    private void OnDisable()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
