using UnityEngine;

public class CameBack : MonoBehaviour
{
    [SerializeField] private BoxCollider _colloder;
    [SerializeField] private float _zDist;
    [SerializeField] private float _startZ;
    private void Awake()
    {
        _zDist = _colloder.size.z * 2;
        _startZ = transform.position.z;
    }

    private void LateUpdate()
    {
        if (_zDist <= _startZ - transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _startZ);
        }
    }
}
