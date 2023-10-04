using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamagable
{
    [SerializeField] private FracturedAstroid _fracturedAsteroidPrefab;
    [SerializeField] private Detonator _explosionPrefab;

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        FractureAstroid(hitPosition);
    }

    private Transform _transform;
    private void Awake() {
        _transform = transform;
    }

    private void FractureAstroid(Vector3 hitposition) {
        if (_fracturedAsteroidPrefab != null) {
            Instantiate(_fracturedAsteroidPrefab, _transform.position, _transform.rotation);
        }
        if (_explosionPrefab != null) {
            Instantiate(_explosionPrefab, hitposition, Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
