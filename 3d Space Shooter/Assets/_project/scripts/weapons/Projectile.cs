using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Detonator _explosionPreFab;

    [SerializeField] AudioClip _impactSound;
    [SerializeField] [Range(5000f, 25000f)]  float _launchForce = 10000f;
    [SerializeField] [Range(10,1000)] int _damage = 100;
    [SerializeField] [Range(2f, 10f)] float _range = 5f; 
    Rigidbody _rigidbody;
    // AudioSource _audioSource;
    float _duration;
    bool OutOfFuel {
        get {
            _duration-= Time.deltaTime;
            return _duration <= 0f;
        }
    }


    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        // _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());
    }

    void OnEnable() {
        _rigidbody.AddForce(_launchForce * transform.forward);
        _duration = _range;
    }

    void Update() {
        if (OutOfFuel) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log($"projectile collided with {collision.collider.name}");
        if (collision.collider.name != "Collider1") {
        Instantiate(_explosionPreFab, transform.position, transform.rotation);
        }
        IDamagable damagable = collision.collider.gameObject.GetComponent<IDamagable>();
        if (damagable != null) {
            Vector3 hitPosition = collision.GetContact(0).point;
            damagable.TakeDamage(_damage, hitPosition);
        }
    }
}
