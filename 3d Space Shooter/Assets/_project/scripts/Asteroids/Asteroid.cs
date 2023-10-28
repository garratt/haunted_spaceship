using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamagable
{
    [SerializeField] float _speed = 15f, _rotateSpeed = 150f;
    [SerializeField] int _damage = 1000;

    [SerializeField] private FracturedAstroid _fracturedAsteroidPrefab;
    [SerializeField] private Detonator _explosionPrefab;
        Rigidbody _rigidbody;

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        FractureAstroid(hitPosition);
    }

// For killer astroids
    public void Init(Transform target){
        _target = target;
    }

    private Transform _transform, _target;
    private void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FractureAstroid(Vector3 hitposition) {
        if (_fracturedAsteroidPrefab != null) {
            Instantiate(_fracturedAsteroidPrefab, _transform.position, _transform.rotation);
        }
        if (_explosionPrefab != null) {
            Instantiate(_explosionPrefab, hitposition, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (_target)
        {
            var direction = _target.position - _transform.position;
        //     var rotation = Quaternion.LookRotation(direction);
        //     _rigidbody.MoveRotation(Quaternion.RotateTowards(_transform.rotation, rotation, _rotateSpeed * Time.fixedDeltaTime));

        // _rigidbody.velocity = _transform.forward * _speed;
        _rigidbody.velocity = direction * .1f;

        }
    }

    void OnCollisionEnter(Collision other)
    {
        
        //if (_impactSound) _audioSource.PlayOneShot(_impactSound);
        if (other.collider.TryGetComponent<IDamagable>(out var damageable))
        {
            damageable.TakeDamage(_damage, other.GetContact(0).point);
        }
        DestroyMissile();
    }

    void DestroyMissile()
    {
        if (_explosionPrefab)
        {
            Instantiate(_explosionPrefab, _transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
