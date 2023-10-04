using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] [Required] Projectile _projectilePrefab;
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = .25f;
    
    bool CanFire {
        get {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

    float _coolDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_coolDown > 0 ) {
            _coolDown -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0))
        {
            FireProjectile();
        }
        
    }

    public void FireProjectile() 
    {
        if (_coolDown > 0) {
            return;
        }
        _coolDown = _coolDownTime;
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }
}
