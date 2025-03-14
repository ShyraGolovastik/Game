using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class PlayerMover : Prefab
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpHeight;


    private float _horizontal;
    private float _vertical;

    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private float _hp;
    
    public Action<float> OnHealthChanged;
    public float Scores { get; set; }
    public float Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            OnHealthChanged?.Invoke(value);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        OnHealthChanged?.Invoke(Hp);
    }

    private void Update()
    {
        Move();
        if (Physics.Raycast(transform.position, Vector3.down, 0.25f))
        {
            _moveDirection.y = 0.0f;
            if(Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        else
        {
            _moveDirection.y -= Physics.gravity.magnitude * Time.deltaTime;
        }

        if(Hp == 0)
        {
            _rb.isKinematic = false;
            _rb.AddForce(Vector3.back * 40.0f, ForceMode.VelocityChange);
        }
        transform.Translate(_moveDirection*Time.deltaTime);
    }

    private void Move()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(_horizontal * _moveSpeed, _moveDirection.y, _vertical * _moveSpeed);
    }

    private void Jump()
    {
        _moveDirection.y = Mathf.Sqrt(2 * _jumpHeight * Physics.gravity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>())
        {
            Hp--;
            collision.gameObject.GetComponent<Obstacle>().ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Heart>())
        {
            Hp++;
            other.gameObject.GetComponent<Heart>().ReturnToPool();
        }
    }
}
